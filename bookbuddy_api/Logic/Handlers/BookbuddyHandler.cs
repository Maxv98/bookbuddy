using Interfaces.Models;
using Interfaces.Repos;
using Logic.Containers;
using Logic.Entities;

namespace Logic.Handlers
{
    public class BookbuddyHandler
    {
        private readonly IBookbuddyRepo bookbuddyRepo;

        public BookbuddyHandler(IBookbuddyRepo bookBuddyRepo)
        {
            this.bookbuddyRepo = bookBuddyRepo;
        }

        public async Task<int> Add(Bookbuddy bookbuddy)
        {
            BookbuddyModel model = bookbuddy.ToModel();
            return await bookbuddyRepo.Add(model);
        }

        public async Task<Bookbuddy?> GetBookBuddy(int id)
        {
            try
            {
                BookbuddyModel? model = await bookbuddyRepo.Get(id);
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
            List<BookbuddyModel> models = await bookbuddyRepo.GetAll();
            List<Bookbuddy> bookbuddies = new List<Bookbuddy>();
            foreach (BookbuddyModel model in models)
            {
                bookbuddies.Add(new Bookbuddy(model));
            }
            return bookbuddies;
        }

        public async Task<int> DeleteBookbuddy(Bookbuddy bookbuddy)
        {
            BookbuddyModel model = bookbuddy.ToModel();
            return await bookbuddyRepo.Delete(model);
        }
    }
}
