using GalleryProject.DTOs;
using GalleryProject.Models;
using GalleryProject.Repositories.Interfaces;
using GalleryProject.Services.Interfaces;

public class PhotoService : IPhotoService
{
    private readonly IPhotoRepository _photoRepository;
    private readonly ITokenService _tokenService;

    public PhotoService(IPhotoRepository photoRepository, ITokenService tokenService)
    {
        _photoRepository = photoRepository;
        _tokenService = tokenService;
    }

    public async Task<IEnumerable<PhotoResponseDto>> GetAllAsync(string token)
    {
        var userId = _tokenService.GetUserIdFromToken(token);
        var photos = await _photoRepository.GetAllAsync(userId);

        return photos.Select(photo => new PhotoResponseDto
        {
            Id = photo.Id,
            FileName = photo.FileName,
            FilePath = photo.FilePath,
            UploadedAt = photo.UploadedAt,
            UserId = photo.UserId,
            FolderId = photo.FolderId
        });
    }

    public async Task<PhotoResponseDto?> GetByIdAsync(int id, string token)
    {
        var userId = _tokenService.GetUserIdFromToken(token);
        var photo = await _photoRepository.GetByIdAsync(id);
        if (photo.UserId != userId) return null;


        return new PhotoResponseDto
        {
            Id = photo.Id,
            FileName = photo.FileName,
            FilePath = photo.FilePath,
            UploadedAt = photo.UploadedAt,
            UserId = photo.UserId,
            FolderId = photo.FolderId
        };
    }

    public async Task<Photo> AddAsync(PhotoDto dto, string token)
    {
        var userId = _tokenService.GetUserIdFromToken(token);

        var photo = new Photo
        {
            FileName = dto.FileName,
            FilePath = dto.FilePath,
            UserId = userId,
            FolderId = dto.FolderId,
            UploadedAt = DateTime.UtcNow
        };

        var created = await _photoRepository.AddAsync(photo);
        
        return created;
    }

    public async Task<bool> DeleteAsync(int id, string token)
    {
        var userId = _tokenService.GetUserIdFromToken(token);
        var photo = await _photoRepository.GetByIdAsync(id);
        if (photo.UserId != userId) return false;

        return await _photoRepository.DeleteAsync(id);
    }
}