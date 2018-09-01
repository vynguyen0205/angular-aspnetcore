using System.ComponentModel.DataAnnotations;

namespace AddressBook.Service.Dtos.In
{
    public class TagInputDto
    {
        public int TagId { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
