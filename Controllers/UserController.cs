using GalleryProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GalleryProject.Controllers;
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService =  userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromHeader(Name = "Authorization")] string token) 
        => Ok(await _userService.GetAllAsync(token));

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromHeader(Name = "Authorization")] string token, int id)
    {
        var user = await _userService.GetByIdAsync(token, id);
        if (user == null) 
            return NotFound();
        return Ok(user);
    }


    [HttpPut("{targetUserId}/{newRoleId}")]
    public async Task<IActionResult> UpdateRole(
        [FromHeader(Name = "Authorization")] string token, 
        int targetUserId, 
        int newRoleId)
    {
        var result = await _userService.UpdateRoleAsync(token, targetUserId, newRoleId);
        return Ok(result);
    }
    
    [HttpDelete("{userId}")]
    public async Task<IActionResult> Delete([FromHeader(Name = "Authorization")] string token, int userId)
    {
        var result = await _userService.DeleteAsync(userId, token);
        if (!result) return NotFound();
        return NoContent();
    }
}