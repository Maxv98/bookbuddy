using Interfaces.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class BookbuddyContext : DbContext
    {
        private readonly bool _useInMemoryDatabase;

        public BookbuddyContext(DbContextOptions<BookbuddyContext> options, bool useInMemoryDatabase = false)
        : base(options)
        {
            _useInMemoryDatabase = useInMemoryDatabase;
        }

        public DbSet<BookbuddyModel> BookBuddies { get; set; }
        public DbSet<PostModel> Posts { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_useInMemoryDatabase)
            {
                optionsBuilder.UseInMemoryDatabase("BookbuddyDbInMemory");
            }
            else
            {
                optionsBuilder.UseSqlServer();
            }
        }

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
