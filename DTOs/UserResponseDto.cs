using GalleryProject.Models;

namespace GalleryProject.DTOs;

public class UserResponseDto
{
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
    
}