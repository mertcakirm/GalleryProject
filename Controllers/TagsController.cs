using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using GalleryProject.Models;
using GalleryProject.Repositories;
using GalleryProject.Repositories.Interfaces;

namespace GalleryProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TagsController : ControllerBase
    {
        private readonly ITagRepository _tagRepository;

        public TagsController(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _tagRepository.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var tag = await _tagRepository.GetByIdAsync(id);
            if (tag == null) return NotFound();
            return Ok(tag);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Tag tag)
        {
            var created = await _tagRepository.AddAsync(tag);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Tag tag)
        {
            if (id != tag.Id) return BadRequest();
            var updated = await _tagRepository.UpdateAsync(tag);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _tagRepository.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}