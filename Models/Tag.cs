using System.ComponentModel.DataAnnotations;

namespace GalleryProject.Models;

public class Tag
{
    public int Id { get; set; }
    [MaxLength(255)]
    public string Name { get; set; }
    
    public int UserId { get; set; }
    public User User { get; set; }

    // Navigation
    public ICollection<PhotoTag> PhotoTags { get; set; }
}