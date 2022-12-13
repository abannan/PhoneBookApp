using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneBook.Models.Entities
{
    public class Person : BaseClass
    {
        #region Public Properties

        public int ApplicationUserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<ContactDetail>? ContactDetails { get; set; }

        [ForeignKey(nameof(ApplicationUserId))]
        [InverseProperty("Contacts")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        #endregion


    }
}