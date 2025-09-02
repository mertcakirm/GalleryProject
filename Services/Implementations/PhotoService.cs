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

    public async Task<IEnumerable<Photo>> GetAllAsync(string token)
    {
        var userId = _tokenService.GetUserIdFromToken(token);
        Console.WriteLine(userId);
        
        return await _photoRepository.GetAllAsync(userId);
    }

    public async Task<Photo?> GetByIdAsync(int id, string token)
    {
        var userId = _tokenService.GetUserIdFromToken(token);
        return await _photoRepository.GetByIdAsync(id, userId);
    }

    public async Task<Photo> AddAsync(PhotoDto dto, string token)
    {
        var userId = _tokenService.GetUserIdFromToken(token);

        var photo = new Photo
        {
            FileName = dto.FileName,
            FilePath = dto.FilePath,
            UserId = userId,
            FolderId = dto.FolderId,   // null olabilir
            UploadedAt = DateTime.UtcNow
        };

        var created = await _photoRepository.AddAsync(photo);

        if (dto.TagIds != null && dto.TagIds.Any())
        {
            created.PhotoTags = dto.TagIds.Select(tid => new PhotoTag
            {
                PhotoId = created.Id,
                TagId = tid
            }).ToList();

            await _photoRepository.UpdateAsync(created);
        }
        return created;
    }

    public async Task<Photo> UpdateAsync(int id, PhotoDto dto, string token)
    {
        var userId = _tokenService.GetUserIdFromToken(token);
        var existing = await _photoRepository.GetByIdAsync(id, userId);
        if (existing == null) return null;

        existing.FileName = dto.FileName;
        existing.FilePath = dto.FilePath;
        existing.FolderId = dto.FolderId;  // null olabilir

        // Tag güncellemesi
        if (dto.TagIds != null)
        {
            existing.PhotoTags = dto.TagIds.Select(tid => new PhotoTag
            {
                PhotoId = existing.Id,
                TagId = tid
            }).ToList();
        }

        return await _photoRepository.UpdateAsync(existing);
    }

    public async Task<bool> DeleteAsync(int id, string token)
    {
        var userId = _tokenService.GetUserIdFromToken(token);
        var photo = await _photoRepository.GetByIdAsync(id, userId);
        if (photo == null || photo.UserId != userId)
            return false;

        return await _photoRepository.DeleteAsync(id,userId);
    }
}