using GalleryProject.Models;
using Microsoft.EntityFrameworkCore;
using GalleryProject.Repositories.Interfaces;

namespace GalleryProject.Repositories
{
    public class FolderRepository : IFolderRepository
    {
        private readonly AppDbContext _context;

        public FolderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Folder>> GetAllAsync(int userId)
        {
            return await _context.Folders
                .Include(f => f.User)
                .Include(f => f.Photos)
                .Where(f => f.UserId == userId)
                .OrderBy(f=>f.Id)
                .ToListAsync();
        }

        public async Task<Folder> GetByIdAsync(int id)
        {
            return await _context.Folders
                .Include(f => f.User)
                .Include(f => f.Photos)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<Folder> AddAsync(Folder folder)
        {
            await _context.Folders.AddAsync(folder);
            await _context.SaveChangesAsync();
            return folder;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var folder = await _context.Folders.FindAsync(id);
            if (folder == null) return false;

            _context.Folders.Remove(folder);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}