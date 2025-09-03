using GalleryProject.DTOs;
using GalleryProject.Models;

namespace GalleryProject.Repositories.Interfaces;


public interface IPhotoService
{
    Task<IEnumerable<PhotoResponseDto>> GetAllAsync(string token);
    Task<PhotoResponseDto?> GetByIdAsync(int id, string token);
    Task<Photo> AddAsync(PhotoDto photo, string token);
    Task<bool> DeleteAsync(int photoId, string token);
}