using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AddressBook.Data.Entities;
using AddressBook.Data.Interfaces;
using AddressBook.Service.BusinessServices;
using AddressBook.Service.Dtos.In;
using AddressBook.Service.Dtos.Out;

namespace AddressBook.ServiceImplementation.BusinessServices
{
    public class TagService: ITagService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TagService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<TagDto> GetTags()
        {
            var allTags = _unitOfWork.TagRepository.All().Select(tag => new TagDto
            {
                TagId = tag.TagId,
                TagName = tag.TagName
            });

            return allTags;
        }

        public async Task<Tag> AddTag(TagInputDto model)
        {
            var newTag = new Tag
            {
                TagName = model.Name
            };

            _unitOfWork.TagRepository.Add(newTag);

            await _unitOfWork.SaveChangeAsync();
            return newTag;
        }

        public async Task<Tag> UpdateTag(TagInputDto model)
        {
            var tag = _unitOfWork.TagRepository.FindBy(t => t.TagId == model.TagId).FirstOrDefault();

            if (tag == null)
            {
                throw new ArgumentNullException($"Tag with Id {model.TagId} not found.");
            }

            tag.TagName = model.Name;
            
            _unitOfWork.TagRepository.Update(tag);
            await _unitOfWork.SaveChangeAsync();

            return tag;
        }

        public async Task DeleteTag(int tagId)
        {
            var tag = _unitOfWork.TagRepository.FindBy(t => t.TagId == tagId).FirstOrDefault();

            if (tag == null)
            {
                throw new ArgumentNullException($"Tag with Id {tagId} not found.");

            }

            var contactTags = _unitOfWork.ContactTagRepository
                .FindBy(contactTag => contactTag.TagId == tagId).ToList();

            foreach (var contactTag in contactTags)
            {
                _unitOfWork.ContactTagRepository.Delete(contactTag);
            }

            _unitOfWork.TagRepository.Delete(tag);
            await _unitOfWork.SaveChangeAsync();
        }
    }
}
