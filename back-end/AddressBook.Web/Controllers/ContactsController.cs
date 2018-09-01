using System;
using System.Threading.Tasks;
using AddressBook.Service.BusinessServices;
using AddressBook.Service.Dtos.In;
using Microsoft.AspNetCore.Mvc;

namespace AddressBook.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/Contacts")]
    public class ContactsController : Controller
    {
        private readonly IContactService _contactService;

        public ContactsController(IContactService contactService)
        {
            _contactService = contactService;
        }

        // GET: api/Contacts
        [HttpGet(Name = "GetContacts")]
        public IActionResult Get([FromQuery]Paging paging)
        {
            var contacts = _contactService.GetAll(paging);

            return Ok(contacts);
        }

        // GET: api/Contacts/5
        [HttpGet("{contactId}", Name = "GetContact")]
        public IActionResult Get(int contactId)
        {
            var contact = _contactService.GetContactById(contactId);

            return Ok(contact);
        }
        
        // POST: api/Contacts
        [HttpPost]
        public void Post([FromBody]string value)
        {
            throw new NotImplementedException();
        }
        
        // PUT: api/Contacts/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
            throw new NotImplementedException();
        }

        // PATCH: api/Contacts/5
        [HttpPatch("{contactId}")]
        public async Task<IActionResult> Patch(int contactId, [FromBody] ContactTagUpdateDto model)
        {
            model.ContactId = contactId;
            var result = await _contactService.ModifyContactTag(model);

            return Ok(result);
        }

        // DELETE: api/Contacts/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
