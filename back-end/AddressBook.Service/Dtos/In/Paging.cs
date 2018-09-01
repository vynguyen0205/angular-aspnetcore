using System.ComponentModel.DataAnnotations;

namespace AddressBook.Service.Dtos.In
{
    public class Paging
    {
        public string KeyWord { get; set; }

        public int TagId { get; set; }

        [Required]
        public int PageNo { get; set; }

        [Required]
        public int ItemsPerPage { get; set; }
    }
}
