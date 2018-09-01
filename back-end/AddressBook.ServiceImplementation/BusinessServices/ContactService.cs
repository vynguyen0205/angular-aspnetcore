using System;
using System.Linq;
using System.Threading.Tasks;
using AddressBook.Data.Entities;
using AddressBook.Data.Interfaces;
using AddressBook.Service.BusinessServices;
using AddressBook.Service.Dtos.In;
using AddressBook.Service.Dtos.Out;

namespace AddressBook.ServiceImplementation.BusinessServices
{
    public class ContactService : IContactService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ContactService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public PagingResult<ContactCompactDto> GetAll(Paging paging)
        {
            var matchedContacts = _unitOfWork.ContactRepository.All();

            // If there is a key word, filter by the key word 
            if (!string.IsNullOrWhiteSpace(paging.KeyWord))
            {
                matchedContacts = matchedContacts
                    .Where(contact =>
                        contact.FirstName.Contains(paging.KeyWord) ||
                        contact.LastName.Contains(paging.KeyWord) ||
                        contact.Company.Contains(paging.KeyWord));
            }

            if (paging.TagId != 0)
            {
                matchedContacts = matchedContacts
                    .Where(contact => contact.ContactTags.Any(contactTag => contactTag.TagId == paging.TagId));
            }

            // Count total before paginating
            var countMatchedContacts = matchedContacts.Count();

            // Paginate
            matchedContacts = matchedContacts.Skip((paging.PageNo - 1) * paging.ItemsPerPage).Take(paging.ItemsPerPage);

            // Get the needed fields only to enumerate
            var results = matchedContacts.Select(contact => new ContactCompactDto
            {
                ContactId = contact.ContactId,
                Company = contact.Company,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                Tags = contact.ContactTags.Select(contactTag => new TagDto
                {
                    TagName = contactTag.Tag.TagName,
                    TagId = contactTag.TagId
                })
            });

            // Return the current page, together with total number of items
            return new PagingResult<ContactCompactDto>
            {
                List = results,
                TotalItems = countMatchedContacts
            };
        }

        public ContactDto GetContactById(int contactId)
        {
            var contact = _unitOfWork.ContactRepository.FindBy(con => con.ContactId == contactId)
                .Select(con => new ContactDto
                {
                    ContactId = con.ContactId,
                    FirstName = con.FirstName,
                    LastName = con.LastName,
                    LinkedIn = con.LinkedIn,
                    Company = con.Company,
                    Skype = con.Skype,
                    Email = con.Email,
                    Phone = con.Phone
                })
                .FirstOrDefault();

            if (contact == null)
            {
                throw new ArgumentNullException($"Contact with Id = {contactId} not found.");
            }

            return contact;
        }

        public async Task<bool> ModifyContactTag(ContactTagUpdateDto model)
        {
            var anyExistingContactTag = _unitOfWork.ContactTagRepository
                .FindBy(contactTag => contactTag.ContactId == model.ContactId && contactTag.TagId == model.TagId)
                .FirstOrDefault();

            if (model.IsSelected)
            {
                // Already added, duplicate action
                if (anyExistingContactTag != null)
                {
                    return false;
                }

                _unitOfWork.ContactTagRepository.Add(new ContactTag
                {
                    TagId = model.TagId,
                    ContactId = model.ContactId
                });
            }
            else
            {
                // Already deleted, duplicate action
                if (anyExistingContactTag == null)
                {
                    return false;
                }

                _unitOfWork.ContactTagRepository.Delete(anyExistingContactTag);
            }

            await _unitOfWork.SaveChangeAsync();

            return true;
        }
    }
}
