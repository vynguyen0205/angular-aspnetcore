using System.Threading.Tasks;
using AddressBook.Service.Dtos.In;
using AddressBook.Service.Dtos.Out;

namespace AddressBook.Service.BusinessServices
{
    public interface IContactService
    {
        PagingResult<ContactCompactDto> GetAll(Paging paging);

        Task<bool> ModifyContactTag(ContactTagUpdateDto model);

        ContactDto GetContactById(int contactId);
    }
}
