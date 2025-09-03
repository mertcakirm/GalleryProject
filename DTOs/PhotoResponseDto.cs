namespace GalleryProject.DTOs;

public class PhotoResponseDto
{
    public int Id { get; set; }
    public string FileName { get; set; }
    public string FilePath { get; set; }
    public DateTime UploadedAt { get; set; }
    public int? UserId { get; set; }
    public int? FolderId { get; set; }
}