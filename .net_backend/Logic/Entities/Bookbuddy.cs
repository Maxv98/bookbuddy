using Interfaces.Models;

namespace Logic.Entities
{
    public class Bookbuddy
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public string? AboutMe { get; set; } = null;
        public string? Interests { get; set; } = null;

        public Bookbuddy (BookbuddyModel model)
        {
            Id = model.Id;
            Email = model.Email;
            Password = model.Password;
            Username = model.Username;
            AboutMe = model.AboutMe;
            Interests = model.Interests;
        }

        public BookbuddyModel ToModel()
        {
            return new BookbuddyModel
            {
                Id = this.Id,
                Email = this.Email,
                Password = this.Password,
                Username = this.Username,
                AboutMe = this.AboutMe,
                Interests = this.Interests
            };
        }
    }
}
