namespace GalleryProject.Models;

public class Tag
{
    public int Id { get; set; }
    public string Name { get; set; }

    // Navigation
    public ICollection<PhotoTag> PhotoTags { get; set; }
}