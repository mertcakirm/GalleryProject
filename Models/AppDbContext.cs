using Microsoft.EntityFrameworkCore;

namespace GalleryProject.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Folder> Folders { get; set; }
    public DbSet<Photo> Photos { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<PhotoTag> PhotoTags { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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

        base.OnModelCreating(modelBuilder);
    }
}