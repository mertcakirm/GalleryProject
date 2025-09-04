using GalleryProject.Models;

namespace GalleryProject.Repositories.Interfaces;

public interface IUserRepository
{
    public Task<IEnumerable<User>> GetAllAsync();
    public Task<User?> GetByIdAsync(int userId);
    public Task<bool> UpdateRoleAsync(int userId, int newRoleId);
    public Task<bool> DeleteAsync(int userId);
}