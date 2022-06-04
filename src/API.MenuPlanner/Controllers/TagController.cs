using API.MenuPlanner.Entities;
using API.MenuPlanner.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API.MenuPlanner.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TagController : ControllerBase
    {

        private readonly IRepository<Tag> _tagRepository;

        public TagController(IRepository<Tag> tagRepository)
        {
            _tagRepository = tagRepository;
        }

        [HttpPost]
        public async Task<ActionResult<Tag>> Create(Tag newTag)
        {
            await _tagRepository.AddAsync(newTag);
            return Ok(newTag);
        }

        [HttpGet]
        public async Task<ActionResult<List<Tag>>> Get()
        {
            var tags = await _tagRepository.FindAsync();
            return Ok(tags);
        }
    }
}
