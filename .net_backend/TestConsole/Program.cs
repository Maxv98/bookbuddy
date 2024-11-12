using DataAccessLayer;
using DataAccessLayer.Repositories;
using Interfaces.Models;

namespace TestConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            Task t = TestSomething();
            t.Wait();
        }

        public static async Task TestSomething()
        {
            BookBuddyRepo repo = new BookBuddyRepo(new BookBuddyContext());

            BookBuddyModel model = new BookBuddyModel() { Email = "email", Password = "password", UserName = "username" };

            int x = await repo.AddBookBuddy(model);
            if (x == 1)
            {
                Console.WriteLine("BookBuddy added successfully");
            }
            else
            {
                Console.WriteLine("BookBuddy not added");
            }

            List<BookBuddyModel> bookBuddies = await repo.GetAllBookBuddies();
            foreach (BookBuddyModel buddy in bookBuddies)
            {
                Console.WriteLine(buddy.UserName);
            }

        }
    }
}
