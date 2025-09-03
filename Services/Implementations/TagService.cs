using GalleryProject.DTOs;
using GalleryProject.Models;
using GalleryProject.Repositories.Interfaces;
using GalleryProject.Services.Interfaces;

namespace GalleryProject.Services.Implementations;

public class TagService: ITagService
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
        
        return tags.Select(tag=>new TagResponseDto{Id = tag.Id, Name = tag.Name,UserId = tag.UserId});
    }
    
    public async Task<TagResponseDto> GetByIdAsync(int id,string token)
    {
        var userId = _tokenService.GetUserIdFromToken(token);
        var tags = await _tagRepository.GetByIdAsync(id);
        if (tags.UserId != userId) return null;
        return new TagResponseDto{Id = tags.Id, Name = tags.Name,UserId = tags.UserId};

    }
    
    public async Task<TagResponseDto> AddAsync(TagDto tag,string token)
    {
        var userId = _tokenService.GetUserIdFromToken(token);
        var newTag = new Tag
        {
            Name = tag.Name,
            UserId = userId,
        };
        var created = await _tagRepository.AddAsync(newTag);
        return new TagResponseDto
        {
            Id = created.Id,
            Name = created.Name,
            UserId = created.UserId
        };
    }
    
    public async Task<bool> DeleteAsync(int id ,string token)
    {
        var userId = _tokenService.GetUserIdFromToken(token);
        var tag = await _tagRepository.GetByIdAsync(id);
        if (tag.UserId != userId) return false;
        return await _tagRepository.DeleteAsync(id);
    }

}