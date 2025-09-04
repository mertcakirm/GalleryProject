using GalleryProject.Models;
using GalleryProject.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GalleryProject.Repositories.Implementatios;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;
    
    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Users
            .Include(u => u.Role) 
            .OrderBy(f=>f.UserId)
            .ToListAsync();
    }

    public async Task<User?> GetByIdAsync(int userId)
    {
        return await _context.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.UserId == userId);
    }

    public async Task<bool> DeleteAsync(int userId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null) return false;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateRoleAsync(int userId, int newRoleId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null) return false;

        // Rol var mı kontrol et
        var roleExists = await _context.Roles.AnyAsync(r => r.RoleId == newRoleId);
        if (!roleExists) return false;

        user.RoleId = newRoleId;
        await _context.SaveChangesAsync();
        return true;
    }
}