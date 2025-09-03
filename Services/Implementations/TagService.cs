using GalleryProject.DTOs;
using GalleryProject.Models;
using GalleryProject.Repositories.Interfaces;
using GalleryProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GalleryProject.Services.Implementations;

public class TagService : ITagService
{
    private readonly ITagRepository _tagRepository;
    private readonly ITokenService _tokenService;

    public TagService(ITagRepository tagRepository, ITokenService tokenService)
    {
        _tagRepository = tagRepository;
        _tokenService = tokenService;
    }

    public async Task<IEnumerable<TagResponseDto>> GetAllAsync(string token)
    {
        var userId = _tokenService.GetUserIdFromToken(token);
        var tags = await _tagRepository.GetAllAsync(userId);

        return tags.Select(tag => new TagResponseDto
        {
            Id = tag.Id,
            Name = tag.Name,
            UserId = tag.UserId
        });
    }

    public async Task<TagResponseDto> GetByIdAsync(int id, string token)
    {
        var userId = _tokenService.GetUserIdFromToken(token);
        var tag = await _tagRepository.GetByIdAsync(id);

        if (tag == null)
            throw new KeyNotFoundException($"ID {id} olan tag bulunamadı.");

        if (tag.UserId != userId)
            throw new UnauthorizedAccessException("Bu tag'e erişim yetkiniz yok.");

        return new TagResponseDto
        {
            Id = tag.Id,
            Name = tag.Name,
            UserId = tag.UserId
        };
    }

    public async Task<TagResponseDto> AddAsync(TagDto dto, string token)
    {
        var userId = _tokenService.GetUserIdFromToken(token);

        var newTag = new Tag
        {
            Name = dto.Name,
            UserId = userId
        };

        try
        {
            var created = await _tagRepository.AddAsync(newTag);
            return new TagResponseDto
            {
                Id = created.Id,
                Name = created.Name,
                UserId = created.UserId
            };
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException($"'{dto.Name}' adında bir tag zaten mevcut.", ex);
        }
    }

    public async Task<bool> DeleteAsync(int id, string token)
    {
        var userId = _tokenService.GetUserIdFromToken(token);
        var tag = await _tagRepository.GetByIdAsync(id);

        if (tag == null)
            throw new KeyNotFoundException($"ID {id} olan tag bulunamadı.");

        if (tag.UserId != userId)
            throw new UnauthorizedAccessException("Bu tag'i silme yetkiniz yok.");

        var deleted = await _tagRepository.DeleteAsync(id);
        if (!deleted)
            throw new InvalidOperationException("Tag silinemedi, bir sorun oluştu.");
        return true;
    }
    
}