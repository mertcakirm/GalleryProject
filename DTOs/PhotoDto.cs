namespace GalleryProject.DTOs;

public class PhotoDto
{
    public int Id { get; set; }
    public string FileName { get; set; }
    public string FilePath { get; set; }
    public int? UserId { get; set; }
    public int? FolderId { get; set; }
    public List<int>? TagIds { get; set; }
    public IFormFile File { get; set; } = null!;
}