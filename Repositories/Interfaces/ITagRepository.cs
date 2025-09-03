using GalleryProject.Models;

namespace GalleryProject.Repositories.Interfaces;

public interface ITagRepository
{
    public Task<IEnumerable<Tag>> GetAllAsync(int userId);
    public Task<Tag> GetByIdAsync(int id);
    public Task<Tag> AddAsync(Tag tag);
    public Task<bool> DeleteAsync(int id);
}