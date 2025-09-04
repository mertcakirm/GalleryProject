using GalleryProject.DTOs;
using GalleryProject.Models;
using GalleryProject.Repositories.Interfaces;
using GalleryProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GalleryProject.Controllers;

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        private readonly ITokenService _tokenService;

        public AuthController(IAuthRepository authRepository, ITokenService tokenService)
        {
            _authRepository = authRepository;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto request)
        {
            if (await _authRepository.UserExists(request.Email))
                return BadRequest("Bu email zaten kayıtlı!");

            var newUser = new User
            {
                UserName = request.UserName,
                Email = request.Email,
                RoleId = 1 
            };

            var createdUser = await _authRepository.Register(newUser, request.Password);

            return Ok(new
            {
                createdUser.UserId,
                createdUser.Email,
                createdUser.UserName
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto request)
        {
            var user = await _authRepository.Login(request.Email, request.Password);
            if (user == null)
                return Unauthorized("Email veya parola hatalı!");

            var tokenString = _tokenService.CreateToken(user);

            return Ok(tokenString);
        }
        
    }
