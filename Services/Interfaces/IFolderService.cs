using GalleryProject.DTOs;

namespace GalleryProject.Services.Interfaces;

public interface IFolderService
{
    public Task<IEnumerable<FolderResponseDto>> GetAllAsync(string token);
    public Task<FolderResponseDto?> GetByIdAsync(int id, string token);
    public Task<FolderResponseDto> AddAsync(FolderDto folder, string token);
    public Task<bool> DeleteAsync(int id, string token);
}