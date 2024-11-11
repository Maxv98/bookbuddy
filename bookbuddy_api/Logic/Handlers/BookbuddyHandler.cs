using Interfaces.Models;
using Interfaces.Repos;
using Logic.Containers;
using Logic.Entities;

namespace Logic.Handlers
{
    public class BookbuddyHandler
    {
        private readonly IBookBuddyRepo bookBuddyRepo;

        public BookbuddyHandler(IBookBuddyRepo bookBuddyRepo)
        {
            this.bookBuddyRepo = bookBuddyRepo;
        }

        public async Task<int> AddBookbuddy(Bookbuddy bookBuddy)
        {
            BookbuddyModel model = bookBuddy.ToModel();
            return await bookBuddyRepo.Add(model);
        }

        public async Task<Bookbuddy?> GetBookBuddy(int id)
        {
            try
            {
                BookbuddyModel? model = await bookBuddyRepo.Get(id);
                if (model != null)
                { return new Bookbuddy(model); }
                else
                { return null; }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Bookbuddy>> GetAllBookBuddies()
        {
            List<BookbuddyModel> models = await bookBuddyRepo.GetAll();
            List<Bookbuddy> bookBuddies = new List<Bookbuddy>();
            foreach (BookbuddyModel model in models)
            {
                bookBuddies.Add(new Bookbuddy(model));
            }
            return bookBuddies;
        }

        public async Task<int> DeleteBookBuddy(Bookbuddy bookBuddy)
        {
            BookbuddyModel model = bookBuddy.ToModel();
            return await bookBuddyRepo.Delete(model);
        }
    }
}
