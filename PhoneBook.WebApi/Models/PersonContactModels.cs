using System.ComponentModel.DataAnnotations;

namespace PhoneBook.WebApi.Models
{
    public class PersonModel
    {
        [Required]
        public int ApplicationUserId { get; set; }
        
        public int? Id { get; set; }

        [Required]
        public string FirstName { get; set; }
        
        [Required]
        public string LastName { get; set; }

        [Required]
        public ICollection<ContactDetailModel> ContactDetails { get; set; }
    }

    public class ContactDetailModel
    {

        public int Id { get; set; }

        [Required]
        public int PersonId { get; set; }

        [Required]
        public int ContactType { get; set; }

        [Required]
        public string ContactValue { get; set; }
    }
}
