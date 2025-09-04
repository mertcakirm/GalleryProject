using GalleryProject.DTOs;
using GalleryProject.Models;

namespace GalleryProject.Services.Interfaces;

public interface IUserService
{
    public Task<IEnumerable<UserResponseDto?>> GetAllAsync(string token);
    public Task<UserResponseDto?> GetByIdAsync(string token, int id);
    public Task<bool> UpdateRoleAsync(string token, int targetUserId , int newRoleId);
    public Task<bool> DeleteAsync(int userid, string token);
}