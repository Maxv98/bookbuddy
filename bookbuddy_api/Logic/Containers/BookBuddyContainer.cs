using Logic.Entities;

namespace Logic.Containers
{
    public class BookBuddyContainer
    {
        public static bool ValidateLogin(Bookbuddy bookBuddy, string password)
        {
            return bookBuddy.Password == password;
        }
    }
}
