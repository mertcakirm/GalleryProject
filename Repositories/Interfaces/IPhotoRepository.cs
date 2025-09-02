using GalleryProject.Models;

namespace GalleryProject.Repositories.Interfaces;


    public interface IPhotoRepository
    {
        Task<IEnumerable<Photo>> GetAllAsync(int userId);
        Task<Photo> GetByIdAsync(int id,int userId);
        Task<Photo> AddAsync(Photo photo);
        Task<Photo> UpdateAsync(Photo photo);
        Task<bool> DeleteAsync(int photoId,int userId);
    }
