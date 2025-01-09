using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Interfaces.Models
{
    public class BookbuddyModel
    {
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        public required string Username { get; set; }

        [Required]
        public required string Password { get; set; }


        public ICollection<PostModel> Posts { get; set; } = new List<PostModel>();

        public ICollection<PostModel> SavedPosts { get; set; } = new List<PostModel>();
    }
}
