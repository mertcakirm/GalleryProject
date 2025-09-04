using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GalleryProject.Models
{
    public class User
    {
        public User()
        {
            Folders = new HashSet<Folder>();
            Photos = new HashSet<Photo>();
        }

        public int UserId { get; set; }

        [MaxLength(255)]
        public string UserName { get; set; }

        [MaxLength(255)]
        public string Email { get; set; }

        public string ProfileImage { get; set; } = "https://static.vecteezy.com/system/resources/thumbnails/005/544/718/small_2x/profile-icon-design-free-vector.jpg";

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public int RoleId { get; set; } = 1;
        public Role Role { get; set; }

        public ICollection<Folder> Folders { get; set; }
        public ICollection<Photo> Photos { get; set; }
    }
}