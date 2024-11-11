using Interfaces.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class BookBuddyContext : DbContext
    {
        public BookBuddyContext(DbContextOptions<BookBuddyContext> options) : base(options) { }

        public DbSet<BookbuddyModel> BookBuddies { get; set; }
        public DbSet<PostModel> Posts { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<BookbuddyModel>()
                .HasIndex(u => u.Email)
                .IsUnique();
            builder.Entity<BookbuddyModel>()
                .HasIndex(u => u.Username)
                .IsUnique();
        }
    }
}
