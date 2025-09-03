using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using GalleryProject.DTOs;
using GalleryProject.Models;
using GalleryProject.Repositories;
using GalleryProject.Repositories.Interfaces;
using GalleryProject.Services.Interfaces;

namespace GalleryProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FoldersController : ControllerBase
    {
        private readonly IFolderService _folderService;

        public FoldersController(IFolderService folderService)
        {
            _folderService = folderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromHeader(Name = "Authorization")] string token) => Ok(await _folderService.GetAllAsync(token));

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id,[FromHeader(Name = "Authorization")] string token)
        {
            var folder = await _folderService.GetByIdAsync(id,token);
            if (folder == null) return NotFound();
            return Ok(folder);
        }

        [HttpPost]
        public async Task<IActionResult> Create(FolderDto folder,[FromHeader(Name = "Authorization")] string token)
        {
            var created = await _folderService.AddAsync(folder,token);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id,[FromHeader(Name = "Authorization")] string token)
        {
            var result = await _folderService.DeleteAsync(id,token);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}