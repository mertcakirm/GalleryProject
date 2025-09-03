using GalleryProject.DTOs;
using GalleryProject.Models;

namespace GalleryProject.Services.Interfaces;

public interface ITagService
{
    public Task<IEnumerable<TagResponseDto>> GetAllAsync(string token);
    public Task<TagResponseDto> GetByIdAsync(int id, string token);
    public Task<TagResponseDto?> AddAsync(TagDto tag, string token);
    public Task<bool> DeleteAsync(int id, string token);
}