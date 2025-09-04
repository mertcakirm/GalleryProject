using Microsoft.EntityFrameworkCore;

namespace GalleryProject.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Folder> Folders { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<PhotoTag> PhotoTags { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // PhotoTag (Many-to-Many)
            modelBuilder.Entity<PhotoTag>()
                .HasKey(pt => new { pt.PhotoId, pt.TagId });

            modelBuilder.Entity<PhotoTag>()
                .HasOne(pt => pt.Photo)
                .WithMany(p => p.PhotoTags)
                .HasForeignKey(pt => pt.PhotoId);

            modelBuilder.Entity<PhotoTag>()
                .HasOne(pt => pt.Tag)
                .WithMany(t => t.PhotoTags)
                .HasForeignKey(pt => pt.TagId);

            // Tag Name unique
            modelBuilder.Entity<Tag>()
                .HasIndex(t => t.Name)
                .IsUnique();

            // Photo FileName unique
            modelBuilder.Entity<Photo>()
                .HasIndex(p => p.FileName)
                .IsUnique();

            // Folder: UserId + Name unique
            modelBuilder.Entity<Folder>()
                .HasIndex(f => new { f.UserId, f.Name })
                .IsUnique()
                .HasDatabaseName("IX_Folders_UserId_Name");

            // User: ProfileImage default value
            modelBuilder.Entity<User>()
                .Property(u => u.ProfileImage)
                .HasDefaultValue("https://static.vecteezy.com/system/resources/thumbnails/005/544/718/small_2x/profile-icon-design-free-vector.jpg");

            // Role: RoleName default = "user"
            modelBuilder.Entity<Role>()
                .Property(r => r.RoleName)
                .HasDefaultValue("user");

            // User-Role relationship
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict)  // Cascade delete önlemek için
                .HasConstraintName("users_ibfk_1");

            base.OnModelCreating(modelBuilder);
        }
    }
}