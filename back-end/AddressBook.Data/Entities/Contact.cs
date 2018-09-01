using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AddressBook.Data.Entities
{
    public class Contact
    {
        public int ContactId { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        [StringLength(256)]
        public string Email { get; set; }

        [StringLength(256)]
        public string LinkedIn { get; set; }

        [StringLength(100)]
        public string Skype { get; set; }

        [StringLength(100)]
        public string Company { get; set; }

        public virtual ICollection<ContactTag> ContactTags { get; set; }
    }
}
