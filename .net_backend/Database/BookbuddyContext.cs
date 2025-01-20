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

        public DbSet<BookbuddyModel> Bookbuddies { get; set; }
        public DbSet<PostModel> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_useInMemoryDatabase || Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Testing")
            {
                optionsBuilder.UseInMemoryDatabase("BookbuddyDbInMemory");
            }
            else
            {
                optionsBuilder.UseSqlServer();
            }
        }

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
