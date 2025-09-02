using GalleryProject.Models;

namespace GalleryProject.Services.Interfaces;

public interface ITokenService
{
    string CreateToken(User user);
    int GetUserIdFromToken(string token);
}