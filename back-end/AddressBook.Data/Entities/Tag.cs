using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AddressBook.Data.Entities
{
    public class Tag
    {
        public int TagId { get; set; }

        [Required]
        [StringLength(100)]
        public string TagName { get; set; }

        public virtual ICollection<ContactTag> ContactTags { get; set; }
    }
}
