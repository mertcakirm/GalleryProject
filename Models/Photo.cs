using System.Text.Json.Serialization;

namespace GalleryProject.Models;

public class Photo
{
    public int Id { get; set; }
    public string FileName { get; set; }
    public string FilePath { get; set; }
    public DateTime UploadedAt { get; set; }

    public int? UserId { get; set; }
    public User User { get; set; }

    public int? FolderId { get; set; }
    public Folder Folder { get; set; }
    [JsonIgnore]
    // Tags many-to-many
    public ICollection<PhotoTag> PhotoTags { get; set; }
}