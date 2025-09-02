using GalleryProject.DTOs;
using Microsoft.AspNetCore.Mvc;
using GalleryProject.Models;
using GalleryProject.Repositories.Interfaces;

namespace GalleryProject.Controllers;

    [ApiController]
    [Route("api/[controller]")]
    public class PhotosController : ControllerBase
    {
        private readonly IPhotoService _photoService;

        public PhotosController(IPhotoService photoService)
        {
            _photoService = photoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromHeader(Name = "Authorization")] string token) => Ok(await _photoService.GetAllAsync(token));

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromHeader(Name = "Authorization")] string token,int id)
        {
            var photo = await _photoService.GetByIdAsync(id,token);
            if (photo == null) return NotFound();
            return Ok(photo);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromHeader(Name = "Authorization")] string token, [FromBody] PhotoDto dto)
        {
            var created = await _photoService.AddAsync(dto, token);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromHeader(Name = "Authorization")] string token, int id, [FromBody] PhotoDto dto)
        {
            var updated = await _photoService.UpdateAsync(id, dto, token);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{photoId}")]
        public async Task<IActionResult> Delete([FromHeader(Name = "Authorization")] string token, int photoId)
        {
            var result = await _photoService.DeleteAsync(photoId, token);
            if (!result) return NotFound();
            return NoContent();
        }
    }
