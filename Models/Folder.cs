namespace GalleryProject.Models;

public class Folder
{
    public int Id { get; set; }
    public string Name { get; set; }

    // Foreign Key
    public int UserId { get; set; }
    public User User { get; set; }

    // Navigation
    public ICollection<Photo> Photos { get; set; }
}