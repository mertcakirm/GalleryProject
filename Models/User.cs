namespace GalleryProject.Models;

public class User
{
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string ProfileImage { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
    
    public ICollection<Folder> Folders { get; set; }
    public ICollection<Photo> Photos { get; set; }
}