using GalleryProject.Models;
using GalleryProject.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GalleryProject.Repositories.Implementatios;

    public class PhotoRepository : IPhotoRepository
    {
        private readonly AppDbContext _context;

        public PhotoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Photo>> GetAllAsync(int userId)
        {
        Console.WriteLine(userId);
            
            return await _context.Photos
                .Include(p => p.User)
                .Include(p => p.Folder)
                .Include(p => p.PhotoTags)
                .ThenInclude(pt => pt.Tag)
                .Where(u => u.UserId == userId)
                .ToListAsync();
        }

        public async Task<Photo> GetByIdAsync(int id)
        {
            return await _context.Photos
                .Include(p => p.User)
                .Include(p => p.Folder)
                .Include(p => p.PhotoTags)
                .ThenInclude(pt => pt.Tag)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Photo> AddAsync(Photo photo)
        {
            await _context.Photos.AddAsync(photo);
            await _context.SaveChangesAsync();
            return photo;
        }
        

        public async Task<bool> DeleteAsync(int photoId)
        {
            var photo = await _context.Photos.FirstOrDefaultAsync(p => p.Id == photoId);
            if (photo == null) return false;

            _context.Photos.Remove(photo);
            await _context.SaveChangesAsync();
            return true;
        }
    }
