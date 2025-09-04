using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GalleryProject.Models
{
    public class Folder
    {
        public Folder()
        {
            Photos = new HashSet<Photo>();
        }

        public int Id { get; set; }

        [MaxLength(255)]
        public string Name { get; set; }

        // Foreign Key
        public int UserId { get; set; }
        public User User { get; set; }

        // Navigation
        public ICollection<Photo> Photos { get; set; }
    }
}