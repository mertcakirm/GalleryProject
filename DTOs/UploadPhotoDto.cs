
namespace GalleryProject.DTOs;

public class PhotoUploadDto
{
    public int FolderId { get; set; }
    public IFormFile File { get; set; } = null!;
}