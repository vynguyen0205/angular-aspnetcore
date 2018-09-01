using System.Collections.Generic;

namespace AddressBook.Service.Dtos.Out
{
    public class PagingResult<T> where T:class 
    {
        public int TotalItems { get; set; }

        public IEnumerable<T> List { get; set; }
    }
}
