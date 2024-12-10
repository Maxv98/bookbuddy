using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Models
{
    public class PostModel
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Text { get; set; }
        
        public int BookbuddyId { get; set; }
        public BookbuddyModel Bookbuddy { get; set; } = null!;

        public ICollection<BookbuddyModel> SavedByBookbuddies { get; set; } = new List<BookbuddyModel>();
    }
}
