using GalleryProject.DTOs;
using Microsoft.AspNetCore.Mvc;
using GalleryProject.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace GalleryProject.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]

public class PhotosController : ControllerBase
{
    private readonly IPhotoService _photoService;

    public PhotosController(IPhotoService photoService)
    {
        _photoService = photoService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromHeader(Name = "Authorization")] string token)
        => Ok(await _photoService.GetAllAsync(token));

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromHeader(Name = "Authorization")] string token, int id)
    {
        var photo = await _photoService.GetByIdAsync(id, token);
        if (photo == null) return NotFound();
        return Ok(photo);
    }

    [HttpPost]
    public async Task<IActionResult> Upload(
        [FromHeader(Name = "Authorization")] string token,
        [FromForm] PhotoUploadDto dto)
    {
        var created = await _photoService.UploadAsync(dto, token);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    [HttpDelete("{photoId}")]
    public async Task<IActionResult> Delete([FromHeader(Name = "Authorization")] string token, int photoId)
    {
        var result = await _photoService.DeleteAsync(photoId, token);
        if (!result) return NotFound();
        return NoContent();
    }
}