using GalleryProject.DTOs;
using GalleryProject.Models;
using GalleryProject.Repositories.Interfaces;
using GalleryProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GalleryProject.Services.Implementations;

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

    public async Task<PhotoResponseDto> GetByIdAsync(int id, string token)
    {
        var userId = _tokenService.GetUserIdFromToken(token);
        var photo = await _photoRepository.GetByIdAsync(id);

        if (photo == null)
            throw new KeyNotFoundException($"ID {id} olan fotoğraf bulunamadı.");

        if (photo.UserId != userId)
            throw new UnauthorizedAccessException("Bu fotoğrafa erişim yetkiniz yok.");

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

        try
        {
            var created = await _photoRepository.AddAsync(photo);
            return created;
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException($"'{dto.FileName}' adında bir fotoğraf zaten mevcut.", ex);
        }
    }

    public async Task<bool> DeleteAsync(int id, string token)
    {
        var userId = _tokenService.GetUserIdFromToken(token);
        var photo = await _photoRepository.GetByIdAsync(id);

        if (photo == null)
            throw new KeyNotFoundException($"ID {id} olan fotoğraf bulunamadı.");

        if (photo.UserId != userId)
            throw new UnauthorizedAccessException("Bu fotoğrafı silme yetkiniz yok.");

        var deleted = await _photoRepository.DeleteAsync(id);
        if (!deleted)
            throw new InvalidOperationException("Fotoğraf silinemedi, bir sorun oluştu.");
        return true;
        
    }
}