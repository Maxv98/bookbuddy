using Interfaces.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime;

namespace DataAccessLayer
{
    public class BookbuddyContext : DbContext
    {
        public BookbuddyContext(DbContextOptions<BookbuddyContext> options)
        : base(options)
        {
        }

        public DbSet<BookbuddyModel> Bookbuddies { get; set; }
        public DbSet<PostModel> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookbuddyModel>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<BookbuddyModel>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<BookbuddyModel>()
            .HasMany(b => b.Posts)
            .WithOne(p => p.Bookbuddy)
            .HasForeignKey(p => p.BookbuddyId);

            modelBuilder.Entity<BookbuddyModel>()
            .HasMany(b => b.SavedPosts)
            .WithMany(p => p.SavedByBookbuddies)
            .UsingEntity(j => j.ToTable("BookbuddySavedPosts"));
        }
    }
}
