using System;
using System.Threading.Tasks;
using AddressBook.Service.BusinessServices;
using AddressBook.Service.Dtos.In;
using Microsoft.AspNetCore.Mvc;

namespace AddressBook.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/Tags")]
    public class TagsController : Controller
    {
        private readonly ITagService _tagService;

        public TagsController(ITagService tagService)
        {
            _tagService = tagService;
        }

        // GET: api/Tags
        [HttpGet(Name = "GetTags")]
        public IActionResult Get()
        {
            var allTags = _tagService.GetTags();

            return Ok(allTags);
        }

        // GET: api/Tags/5
        [HttpGet("{id}", Name = "GetTag")]
        public string Get(int id)
        {
            throw new NotImplementedException();
        }

        // POST: api/Tags
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]TagInputDto model)
        {
            var newTag = await _tagService.AddTag(model);
            return Ok(newTag);
        }
        
        // PUT: api/Tags/5
        [HttpPut("{tagId}")]
        public async Task<IActionResult> Put(int tagId, [FromBody]TagInputDto model)
        {
            model.TagId = tagId;
            var updatedTag = await _tagService.UpdateTag(model);

            return Ok(updatedTag);
        }
        
        // DELETE: api/Tags/5
        [HttpDelete("{tagId}")]
        public async Task <IActionResult> Delete(int tagId)
        {
            await _tagService.DeleteTag(tagId);
            return Ok();
        }
    }
}
