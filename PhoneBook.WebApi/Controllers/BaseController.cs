using Microsoft.AspNetCore.Mvc;
using PhoneBook.Models;
using PhoneBook.Models.Context;
using PhoneBook.Models.Entities;
using System.Security.Authentication;

namespace PhoneBook.WebApi.Controllers
{
    public class BaseController : Controller
    {
        internal readonly DatabaseContext DatabaseContext;

        public int ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public BaseController(DatabaseContext databaseContext)
        {
            DatabaseContext = databaseContext;
        }

        protected internal void ValidateRequest(string applicationUserIdValue)
        {
            //var applicationUserIdValue = Request.Headers[Constants.Headers.ApplicationUserId].FirstOrDefault();

            //if (applicationUserIdValue == null)
            //{
            //    throw new AuthenticationException("Application User is missing or incorrect.");
            //}

            if (!int.TryParse(applicationUserIdValue, out int applicationUserId))
            {
                throw new AuthenticationException("Application User is missing or incorrect.");
            }

            ApplicationUserId = applicationUserId;

            ApplicationUser = DatabaseContext.ApplicationUser.FirstOrDefault(a => a.Id == applicationUserId);

            if (ApplicationUser == null)
            {
                throw new AuthenticationException("Application User is missing or incorrect.");
            }
        }
    }
}
