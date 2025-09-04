using GalleryProject.DTOs;
using GalleryProject.Repositories.Interfaces;
using GalleryProject.Services.Interfaces;

namespace GalleryProject.Services.Implementations;

public class UserService : IUserService
{
    private readonly ITokenService _tokenService;
    private readonly IUserRepository _userRepository;

    public UserService(ITokenService tokenService, IUserRepository userRepository)
    {
        _tokenService = tokenService;
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<UserResponseDto?>> GetAllAsync(string token)
    {
        var currentUserId = _tokenService.GetUserIdFromToken(token);
        var currentRoleId = _tokenService.GetRoleIdFromToken(token);
        
        if(currentRoleId != 2) throw new InvalidOperationException("Bu işlem için yetkili değilsiniz bulunamadı!");
        
        if (currentUserId == 0) throw new InvalidOperationException("Kullanıcı idsi bulunamadı!");

        var users = await _userRepository.GetAllAsync();

        return users.Select(u => new UserResponseDto
        {
            UserId = u.UserId,
            RoleId = u.RoleId,
            Email = u.Email,
            UserName = u.UserName
        });
    }

    public async Task<UserResponseDto?> GetByIdAsync(string token, int id)
    {
        var currentUserId = _tokenService.GetUserIdFromToken(token);
        var user = await _userRepository.GetByIdAsync(id);
        var currentRoleId = _tokenService.GetRoleIdFromToken(token);
        
        if(currentRoleId != 2) throw new InvalidOperationException("Bu işlem için yetkili değilsiniz bulunamadı!");
        
        if (user == null)
            throw new KeyNotFoundException($"ID {id} olan kullanıcı bulunamadı.");

        if (currentUserId == null)
            throw new UnauthorizedAccessException("Bu kullanıcıya erişim yetkiniz yok.");

        return new UserResponseDto
        {
            UserId = user.UserId,
            RoleId = user.RoleId,
            Email = user.Email,
            UserName = user.UserName
        };
    }

    public async Task<bool> UpdateRoleAsync(string token, int targetUserId, int newRoleId)
    {
        var currentUserId = _tokenService.GetUserIdFromToken(token);
        var currentUser = await _userRepository.GetByIdAsync(currentUserId);
        var currentRoleId = _tokenService.GetRoleIdFromToken(token);
        
        if(currentRoleId != 2) throw new InvalidOperationException("Bu işlem için yetkili değilsiniz bulunamadı!");
        
        if (currentUser == null)
            throw new KeyNotFoundException("Token sahibi kullanıcı bulunamadı.");
        
        if (currentUser.Role.RoleName != "admin")
            throw new UnauthorizedAccessException("Bu kullanıcının rolünü güncelleme yetkiniz yok.");

        var updated = await _userRepository.UpdateRoleAsync(targetUserId, newRoleId);
        if (!updated)
            throw new InvalidOperationException("Kullanıcı rolü güncellenemedi.");

        return true;
    }


    public async Task<bool> DeleteAsync(int targetUserId, string token)
    {
        var currentUserId = _tokenService.GetUserIdFromToken(token);
        var currentUser = await _userRepository.GetByIdAsync(currentUserId);
        
        var currentRoleId = _tokenService.GetRoleIdFromToken(token);
        
        if(currentRoleId != 2) throw new InvalidOperationException("Bu işlem için yetkili değilsiniz bulunamadı!");
        
        if (currentUser == null)
            throw new KeyNotFoundException("Token sahibi kullanıcı bulunamadı.");

        // Sadece admin veya kendisi silebilir
        if (currentUser.Role.RoleName != "admin" && currentUser.UserId != targetUserId)
            throw new UnauthorizedAccessException("Bu kullanıcıyı silme yetkiniz yok.");

        var deleted = await _userRepository.DeleteAsync(targetUserId);
        if (!deleted)
            throw new InvalidOperationException("Kullanıcı silinemedi.");

        return true;
    }
}