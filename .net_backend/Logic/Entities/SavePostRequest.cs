using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Entities
{
    public class SavePostRequest
    {
        public string Username { get; set; } = null!;
        public int PostId { get; set; }
    }
}
