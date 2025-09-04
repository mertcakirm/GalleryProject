using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace GalleryProject.Models
{
    public class Photo
    {
        public Photo()
        {
            PhotoTags = new HashSet<PhotoTag>();
        }

        public int Id { get; set; }

        [MaxLength(255)]
        public string FileName { get; set; }

        public string FilePath { get; set; }
        public DateTime UploadedAt { get; set; }

        public int? UserId { get; set; }
        public User? User { get; set; }

        public int? FolderId { get; set; }
        public Folder? Folder { get; set; }

        [JsonIgnore]
        public ICollection<PhotoTag> PhotoTags { get; set; }
    }
}