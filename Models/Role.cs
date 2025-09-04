using System.Collections.Generic;

namespace GalleryProject.Models
{
    public class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        public int RoleId { get; set; }
        public string RoleName { get; set; } = "user";

        public ICollection<User> Users { get; set; }
    }
}