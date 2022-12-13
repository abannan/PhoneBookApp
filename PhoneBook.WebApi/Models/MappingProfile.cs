using AutoMapper;
using PhoneBook.Models.Entities;

namespace PhoneBook.WebApi.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<ContactDetailModel, ContactDetail>()
                .ReverseMap();

            CreateMap<PersonModel, Person>()
                .ForMember(d => d.ContactDetails, m => m.MapFrom(p => p.ContactDetails.Select(c => c).ToList()))
                .ReverseMap();
        }
    }
}
