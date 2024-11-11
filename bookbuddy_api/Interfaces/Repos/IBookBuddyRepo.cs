using Interfaces.Models;

namespace Interfaces.Repos
{
    public interface IBookbuddyRepo
    {
        Task<int> Add(BookbuddyModel bookBuddy);
        Task<List<BookbuddyModel>> GetAll();
        Task<BookbuddyModel?> Get(int id);
        Task<int> Update(BookbuddyModel bookBuddy);
        Task<int> Delete(BookbuddyModel bookBuddy);
    }
}