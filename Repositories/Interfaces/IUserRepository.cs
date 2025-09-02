using GalleryProject.Models;

namespace GalleryProject.Repositories.Interfaces;

public interface IUserRepository
{
    Task<User> Register(User user, string password);
    Task<User> Login(string email, string password);
    Task<bool> UserExists(string email);
}