using GalleryProject.DTOs;
using GalleryProject.Models;

namespace GalleryProject.Repositories.Interfaces;

public interface IPhotoService
{
    Task<IEnumerable<Photo>> GetAllAsync(string token);
    Task<Photo?> GetByIdAsync(int id, string token);
    Task<Photo> AddAsync(PhotoDto photo, string token);
    Task<Photo> UpdateAsync(int id,PhotoDto photo, string token);
    Task<bool> DeleteAsync(int photoId, string token);
}