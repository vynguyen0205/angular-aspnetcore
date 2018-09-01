using System.Collections.Generic;

namespace AddressBook.Service.Dtos.Out
{
    public class ContactCompactDto
    {
        public int ContactId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Company { get; set; }

        public IEnumerable<TagDto> Tags { get; set; }
    }
}
