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
        public int BookBuddyId { get; set; }
        public required string Text { get; set; }

        public required BookbuddyModel BookBuddy { get; set; }
    }
}
