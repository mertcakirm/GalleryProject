using GalleryProject.DTOs;
using GalleryProject.Models;
using GalleryProject.Repositories.Interfaces;
using GalleryProject.Services.Interfaces;

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

    public async Task<FolderResponseDto?> GetByIdAsync(int id, string token)
    {
        var userId = _tokenService.GetUserIdFromToken(token);
        var folder = await _folderRepository.GetByIdAsync(id);

        if (folder.UserId != userId) return null;

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

        var created = await _folderRepository.AddAsync(newFolder);

        return new FolderResponseDto
        {
            Id = created.Id,
            Name = created.Name,
            UserId = created.UserId
        };
    }

    public async Task<bool> DeleteAsync(int id, string token)
    {
        var userId = _tokenService.GetUserIdFromToken(token);
        var folder = await _folderRepository.GetByIdAsync(id);
        if (folder.UserId != userId) return false;

        return await _folderRepository.DeleteAsync(id);
    }
}