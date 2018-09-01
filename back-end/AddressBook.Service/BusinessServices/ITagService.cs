using System.Collections.Generic;
using System.Threading.Tasks;
using AddressBook.Data.Entities;
using AddressBook.Service.Dtos.In;
using AddressBook.Service.Dtos.Out;

namespace AddressBook.Service.BusinessServices
{
    public interface ITagService
    {
        IEnumerable<TagDto> GetTags();

        Task<Tag> AddTag(TagInputDto model);

        Task<Tag> UpdateTag(TagInputDto model);

        Task DeleteTag(int tagId);
    }
}
