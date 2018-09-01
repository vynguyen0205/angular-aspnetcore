using System.ComponentModel.DataAnnotations;

namespace AddressBook.Service.Dtos.In
{
    public class ContactTagUpdateDto
    {
        public int ContactId { get; set; }

        [Required]
        public int TagId { get; set; }

        public bool IsSelected { get; set; }
    }
}
