using GalleryProject.DTOs;
using GalleryProject.Models;
using GalleryProject.Repositories.Interfaces;
using GalleryProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GalleryProject.Services.Implementations;

public class FolderService : IFolderService
{
    private readonly IFolderRepository _folderRepository;
    private readonly ITokenService _tokenService;

    public FolderService(IFolderRepository folderRepository, ITokenService tokenService)
    {
        _folderRepository = folderRepository;
        _tokenService = tokenService;
    }

    public async Task<IEnumerable<FolderResponseDto>> GetAllAsync(string token)
    {
        var userId = _tokenService.GetUserIdFromToken(token);
        var folders = await _folderRepository.GetAllAsync(userId);

        return folders.Select(folder => new FolderResponseDto
        {
            Id = folder.Id,
            Name = folder.Name,
            UserId = folder.UserId
        });
    }

    public async Task<FolderResponseDto> GetByIdAsync(int id, string token)
    {
        var userId = _tokenService.GetUserIdFromToken(token);
        var folder = await _folderRepository.GetByIdAsync(id);

        if (folder == null)
            throw new KeyNotFoundException($"ID {id} olan klasör bulunamadı.");

        if (folder.UserId != userId)
            throw new UnauthorizedAccessException("Bu klasöre erişim yetkiniz yok.");

        return new FolderResponseDto
        {
            Id = folder.Id,
            Name = folder.Name,
            UserId = folder.UserId
        };
    }

    public async Task<FolderResponseDto> AddAsync(FolderDto folder, string token)
    {
        var userId = _tokenService.GetUserIdFromToken(token);

        var newFolder = new Folder
        {
            Name = folder.Name,
            UserId = userId
        };

        try
        {
            var created = await _folderRepository.AddAsync(newFolder);
            return new FolderResponseDto
            {
                Id = created.Id,
                Name = created.Name,
                UserId = created.UserId
            };
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException($"'{folder.Name}' adında bir klasör zaten mevcut.", ex);
        }
    }

    public async Task<bool> DeleteAsync(int id, string token)
    {
        var userId = _tokenService.GetUserIdFromToken(token);
        var folder = await _folderRepository.GetByIdAsync(id);

        if (folder == null)
            throw new KeyNotFoundException($"ID {id} olan klasör bulunamadı.");

        if (folder.UserId != userId)
            throw new UnauthorizedAccessException("Bu klasörü silme yetkiniz yok.");

        var deleted = await _folderRepository.DeleteAsync(id);
        if (!deleted)
            throw new InvalidOperationException("Klasör silinemedi, bir sorun oluştu.");
        return true;
    }
}