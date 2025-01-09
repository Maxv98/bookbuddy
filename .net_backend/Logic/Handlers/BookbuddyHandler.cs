using Interfaces.Models;
using Interfaces.Repos;
using Logic.Containers;
using Logic.Entities;

namespace Logic.Handlers
{
    public class BookbuddyHandler
    {
        private readonly IBookbuddyRepo _bookbuddyRepo;

        public BookbuddyHandler(IBookbuddyRepo bookbuddyRepo)
        {
            _bookbuddyRepo = bookbuddyRepo;
        }

        public async Task<int> Add(Bookbuddy bookbuddy)
        {
            BookbuddyModel model = bookbuddy.ToModel();
            return await _bookbuddyRepo.Add(model);
        }

        public async Task<Bookbuddy?> Get(int id)
        {
            BookbuddyModel? model = await _bookbuddyRepo.Get(id);
            if (model != null)
            { return new Bookbuddy(model); }
            else
            { return null; }
        }

        public async Task<List<Bookbuddy>> GetAll()
        {
            List<BookbuddyModel> models = await _bookbuddyRepo.GetAll();
            List<Bookbuddy> bookbuddies = new List<Bookbuddy>();
            foreach (BookbuddyModel model in models)
            {
                bookbuddies.Add(new Bookbuddy(model));
            }
            return bookbuddies;
        }

        public async Task<int> Update(Bookbuddy bookbuddy)
        {
            BookbuddyModel model = bookbuddy.ToModel();
            return await _bookbuddyRepo.Update(model);
        }

        public async Task<bool> DeleteBookbuddy(Bookbuddy bookbuddy)
        {
            BookbuddyModel model = bookbuddy.ToModel();
            return await _bookbuddyRepo.Delete(model);
        }

        public async Task<bool> SavePost (int bookbuddyId, int postId)
        {
            return await _bookbuddyRepo.SavePost(bookbuddyId, postId);
        }
    }
}
