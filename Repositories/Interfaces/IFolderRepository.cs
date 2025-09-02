using GalleryProject.Models;

namespace GalleryProject.Repositories.Interfaces;

public interface IFolderRepository
{
    Task<IEnumerable<Folder>> GetAllAsync();
    Task<Folder> GetByIdAsync(int id);
    Task<Folder> AddAsync(Folder folder);
    Task<Folder> UpdateAsync(Folder folder);
    Task<bool> DeleteAsync(int id);
}