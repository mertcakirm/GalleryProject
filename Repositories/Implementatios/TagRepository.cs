using GalleryProject.Models;
using Microsoft.EntityFrameworkCore;
using GalleryProject.Repositories.Interfaces;

namespace GalleryProject.Repositories.Implementatios;

    public class TagRepository : ITagRepository
    {
        private readonly AppDbContext _context;

        public TagRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync(int userId)
        {
            return await _context.Tags
                .Include(t => t.PhotoTags)
                .ThenInclude(pt => pt.Photo)
                .Where(f=>f.UserId == userId)
                .OrderByDescending(f=>f.Id)
                .ToListAsync();
        }

        public async Task<Tag?> GetByIdAsync(int id)
        {
            return await _context.Tags
                .Include(t => t.PhotoTags)
                .ThenInclude(pt => pt.Photo)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Tag> AddAsync(Tag tag)
        {
            await _context.Tags.AddAsync(tag);
            await _context.SaveChangesAsync();
            return tag;
        }


        public async Task<bool> DeleteAsync(int id)
        {
            var tag = await _context.Tags.FindAsync(id);
            if (tag == null) return false;

            _context.Tags.Remove(tag);
            await _context.SaveChangesAsync();
            return true;
        }
    }
