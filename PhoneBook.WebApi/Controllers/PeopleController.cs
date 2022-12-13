using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using PhoneBook.Models.Context;
using PhoneBook.Models.Entities;
using PhoneBook.WebApi.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace PhoneBook.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PeopleController : BaseController
    {
        private readonly IMapper _mapper;

        public PeopleController(DatabaseContext databaseContext, IMapper mapper) : base(databaseContext)
        {
            _mapper = mapper;
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<IActionResult> Get([FromHeader(Name = "Application-User-Id")][Required] string applicationUserId)
        {
            ValidateRequest(applicationUserId);

            var people = await DatabaseContext.People
                .Include(p => p.ContactDetails)
                .Where(p => p.ApplicationUserId == ApplicationUserId)
                .Select(p => _mapper.Map<PersonModel>(p))
                .ToListAsync();
                
            return Ok(people.OrderBy(p => p.LastName));
           
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> Get([FromHeader(Name = "Application-User-Id")][Required] string applicationUserId, int id)
        {
            ValidateRequest(applicationUserId);

            var person = await DatabaseContext.People
                .Include(p => p.ContactDetails)
                .FirstOrDefaultAsync(p => p.Id == id && p.ApplicationUserId == ApplicationUserId);

            if (person == null)
            {
                return BadRequest("Person not found.");
            }
            return Ok(_mapper.Map<PersonModel>(person));
        }

        [HttpGet("Search/{surname}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBySurname([FromHeader(Name = "Application-User-Id")][Required] string applicationUserId, string surname)
        {
            ValidateRequest(applicationUserId);

            var people = await DatabaseContext.People
                .Include(p => p.ContactDetails)
                .Where(p => p.LastName.Contains(surname) && p.ApplicationUserId == ApplicationUserId)
                .Select(p => _mapper.Map<PersonModel>(p))
                .ToListAsync();

            return Ok(people.OrderBy(p => p.LastName));
        }


        [HttpPut]
        [Produces("application/json")]
        public async Task<IActionResult> PutAsync([FromHeader(Name = "Application-User-Id")][Required] string applicationUserId, [FromBody] PersonModel person)
        {

            ValidateRequest(applicationUserId);

            var existingPerson = await DatabaseContext.People
                .Include(p => p.ContactDetails)
                .FirstOrDefaultAsync(p => p.Id == person.Id && p.ApplicationUserId == ApplicationUserId);

            if (existingPerson == null)
            {
                return BadRequest("Person not found.");
            }

            existingPerson.FirstName = person.FirstName;
            existingPerson.LastName = person.LastName;

            existingPerson.UpdatedDate = DateTime.Now;

            foreach(var contactDetail in existingPerson.ContactDetails) 
            {
                var existingContact = existingPerson.ContactDetails.FirstOrDefault(p => p.Id == contactDetail.Id);
                if (existingContact == null)
                {
                    return BadRequest("Contact not found.");
                }
                existingContact.ContactValue = contactDetail.ContactValue;
                existingContact.UpdatedDate = DateTime.Now;

                DatabaseContext.Entry(contactDetail).State = EntityState.Modified;
            }

            DatabaseContext.Entry(existingPerson).State = EntityState.Modified;

            await DatabaseContext.SaveChangesAsync();
            await DatabaseContext.Entry(existingPerson).GetDatabaseValuesAsync();

            return Ok();

        }


        [HttpPost]
        [Produces("application/json")]
        public async Task<IActionResult> PostAsync([FromHeader(Name = "Application-User-Id")][Required] string applicationUserId, [FromBody] PersonModel contactDetail)
        {
            
            ValidateRequest(applicationUserId);

            var person = _mapper.Map<PersonModel, Person>(contactDetail);

            if (person == null)
            {
                return BadRequest("TODO");
            }

            person.ApplicationUserId = ApplicationUserId;
            person.ApplicationUser = ApplicationUser;
            
            person.CreatedDate = DateTime.Now;
            person.UpdatedDate = DateTime.Now;

            foreach (var contact in person.ContactDetails)
            {
                contact.CreatedDate = DateTime.Now;
                contact.UpdatedDate = DateTime.Now;

                DatabaseContext.Entry(contact).State = EntityState.Added;
            }

            DatabaseContext.Entry(person).State = EntityState.Added;

            await DatabaseContext.SaveChangesAsync();
            await DatabaseContext.Entry(person).GetDatabaseValuesAsync();

            return Ok();

        }


        [HttpDelete("{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> DeleteAsync([FromHeader(Name = "Application-User-Id")][Required] string applicationUserId, int id)
        {
            ValidateRequest(applicationUserId);

            var person = await DatabaseContext.People
                .Include(p => p.ContactDetails)
                .FirstOrDefaultAsync(p => p.Id == id && p.ApplicationUserId == ApplicationUserId);

            if (person == null)
            {
                return BadRequest("Person not found.");
            }

            DatabaseContext.Entry(person).State = EntityState.Deleted;

            await DatabaseContext.SaveChangesAsync();

            var result = await DatabaseContext.People
                .Include(p => p.ContactDetails)
                .Where(p => p.ApplicationUserId == ApplicationUserId)
                .Select(p => _mapper.Map<PersonModel>(p))
                .ToListAsync();

            return Ok(result.OrderBy(p => p.LastName));
        }
    }
}
