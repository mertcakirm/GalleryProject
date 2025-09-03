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

        modelBuilder.Entity<Tag>()
            .HasIndex(t => t.Name)
            .IsUnique();

        modelBuilder.Entity<Photo>()
            .HasIndex(t => t.FileName)
            .IsUnique();

        modelBuilder.Entity<Folder>()
            .HasIndex(f => new { f.UserId, f.Name })
            .IsUnique()
            .HasDatabaseName("IX_Folders_UserId_Name")
            .HasFilter(null); 
        
        base.OnModelCreating(modelBuilder);
    }
}