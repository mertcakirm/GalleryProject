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
            
            return await _context.Photos
                .Include(p => p.User)
                .Include(p => p.Folder)
                .Include(p => p.PhotoTags)
                .ThenInclude(pt => pt.Tag)
                .Where(u => u.UserId == userId)
                .OrderByDescending(f=>f.UploadedAt)
                .ToListAsync();
        }

        public async Task<Photo?> GetByIdAsync(int id)
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
            try
            {
                await _context.Photos.AddAsync(photo);
                await _context.SaveChangesAsync();
                return photo;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"DB kayıt hatası: {ex.InnerException?.Message ?? ex.Message}", ex);
            }
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
