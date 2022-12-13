using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Models.Entities
{
    public class ApplicationUser : BaseClass
    {
        #region Public Properties

        public virtual ICollection<Person>? Contacts { get; set; }


        #endregion
    }
}
