using Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Text { get; set; } = null!;

        public int BookbuddyId { get; set; }

        public Post() { }

        public Post(PostModel model)
        {
            Id = model.Id;
            Title = model.Title;
            Text = model.Text;
            BookbuddyId = model.BookbuddyId;
        }

        public PostModel ToModel()
        {
            return new PostModel
            {
                Id = this.Id,
                Title = this.Title,
                Text = this.Text,
                BookbuddyId = this.BookbuddyId
            };
        }
    }
}
