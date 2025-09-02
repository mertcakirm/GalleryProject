using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using GalleryProject.Models;
using GalleryProject.Repositories;
using GalleryProject.Repositories.Interfaces;

namespace GalleryProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FoldersController : ControllerBase
    {
        private readonly IFolderRepository _folderRepository;

        public FoldersController(IFolderRepository folderRepository)
        {
            _folderRepository = folderRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _folderRepository.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var folder = await _folderRepository.GetByIdAsync(id);
            if (folder == null) return NotFound();
            return Ok(folder);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Folder folder)
        {
            var created = await _folderRepository.AddAsync(folder);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Folder folder)
        {
            if (id != folder.Id) return BadRequest();
            var updated = await _folderRepository.UpdateAsync(folder);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _folderRepository.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}