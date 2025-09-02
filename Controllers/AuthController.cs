using GalleryProject.DTOs;
using GalleryProject.Models;
using GalleryProject.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GalleryProject.Controllers;

    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;

        public UserController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        // POST: api/User/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto request)
        {
            if (await _authRepository.UserExists(request.Email))
                return BadRequest("Bu email zaten kayıtlı!");

            var user = new User
            {
                Email = request.Email,
                UserName = request.UserName
            };

            var createdUser = await _authRepository.Register(user, request.Password);

            return Ok(new
            {
                createdUser.UserId,
                createdUser.Email,
                createdUser.UserName
            });
        }

        // POST: api/User/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto request)
        {
            var user = await _authRepository.Login(request.Email, request.Password);
            if (user == null)
                return Unauthorized("Email veya parola hatalı!");

            // Token oluşturmak istersen buraya ekleyebilirsin

            return Ok(new
            {
                user.UserId,
                user.Email,
                user.UserName
            });
        }

        // GET: api/User/exists?email=example@mail.com
        [HttpGet("exists")]
        public async Task<IActionResult> UserExists([FromQuery] string email)
        {
            var exists = await _authRepository.UserExists(email);
            return Ok(new { exists });
        }
    }



