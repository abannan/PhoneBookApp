using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Models.Entities
{
    public class ContactDetail : BaseClass
    {
        #region Public Properties

        public int PersonId { get; set; }
        public int ContactType { get; set; }
        public string ContactValue { get; set; }

        [ForeignKey(nameof(PersonId))]
        [InverseProperty("ContactDetails")]
        public virtual Person Person { get; set; }

        #endregion

        public ContactDetail()
        {
            ContactType = Constants.ContactType.NotSet;
            ContactValue = string.Empty;
            PersonId = 0;
            Person = new Person();
        }

    }
}
