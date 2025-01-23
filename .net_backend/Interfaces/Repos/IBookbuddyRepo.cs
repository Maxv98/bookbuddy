using Interfaces.Models;

namespace Interfaces.Repos
{
    public interface IBookbuddyRepo
    {
        Task<int> Add(BookbuddyModel bookbuddy);
        Task<List<BookbuddyModel>> GetAll();
        Task<BookbuddyModel> Get(int id);
        Task<int> Update(BookbuddyModel bookbuddy);
        Task<bool> Delete(BookbuddyModel bookbuddy);
        Task<bool> SavePost(string username, int postId);
        Task<bool> CheckEmail(string email);
        Task<bool> CheckUsername(string username);
    }
}