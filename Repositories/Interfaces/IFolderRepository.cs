using GalleryProject.Models;

namespace GalleryProject.Repositories.Interfaces;

public interface IFolderRepository
{
    public Task<IEnumerable<Folder>> GetAllAsync(int userId);
    public Task<Folder> GetByIdAsync(int id);
    public Task<Folder> AddAsync(Folder folder);
    public Task<bool> DeleteAsync(int id);
}