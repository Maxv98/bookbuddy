using Interfaces.Models;

namespace Logic.Entities
{
    public class Bookbuddy
    {
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Username { get; set; } = null!;

        public Bookbuddy()
        {
        }

        public Bookbuddy(string email, string password, string username)
        {
            Email = email;
            Password = password;
            Username = username;
        }

        public Bookbuddy (BookbuddyModel model)
        {
            Id = model.Id;
            Email = model.Email;
            Password = model.Password;
            Username = model.Username;
        }

        public BookbuddyModel ToModel()
        {
            return new BookbuddyModel
            {
                Id = this.Id,
                Email = this.Email,
                Password = this.Password,
                Username = this.Username,
            };
        }
    }
}
