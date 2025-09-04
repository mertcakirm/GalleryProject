using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using GalleryProject.DTOs;
using GalleryProject.Models;
using GalleryProject.Repositories;
using GalleryProject.Repositories.Interfaces;
using GalleryProject.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace GalleryProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TagsController : ControllerBase
    {
        private readonly ITagService _tagService;

        public TagsController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpGet("test-error")]
        public IActionResult TestError()
        {
            throw new Exception("Middleware test exception!");
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromHeader(Name = "Authorization")] string token) => Ok(await _tagService.GetAllAsync(token));

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id,[FromHeader(Name = "Authorization")] string token)
        {
            var tag = await _tagService.GetByIdAsync(id, token);
            if (tag == null) return NotFound();
            return Ok(tag);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromHeader(Name = "Authorization")] string token,TagDto tag)
        {
            var created = await _tagService.AddAsync(tag, token);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id,[FromHeader(Name = "Authorization")] string token)
        {
            var result = await _tagService.DeleteAsync(id,token);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}