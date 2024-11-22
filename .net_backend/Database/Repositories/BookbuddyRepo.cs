using Interfaces.Models;
using Interfaces.Repos;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories
{
    public class BookbuddyRepo
        (BookbuddyContext dbContext) : IBookbuddyRepo
    {
        public async Task<int> Add(BookbuddyModel bookBuddy)
        {
            dbContext.BookBuddies.Add(bookBuddy);

            try
            {
                await dbContext.SaveChangesAsync();
                return bookBuddy.Id;
            }

            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx && (sqlEx.Number == 2627 || sqlEx.Number == 2601))
            {
                // 2627: Violation of PRIMARY KEY constraint
                // 2601: Violation of UNIQUE KEY constraint
                if (sqlEx.Message.Contains("IX_BookBuddies_Email"))
                {
                    return -1; // Email constraint violation
                }
                else if (sqlEx.Message.Contains("IX_BookBuddies_Username"))
                {
                    return -2; // Username constraint violation
                }
                throw; // Rethrow if it's not a known unique constraint violation
            }
        }

        public async Task<BookbuddyModel?> Get(int id)
        {
            return await dbContext.BookBuddies.FindAsync(id);
        }

        public async Task<List<BookbuddyModel>> GetAll()
        {
            return await dbContext.BookBuddies.ToListAsync();
        }

        public async Task<int> Update(BookbuddyModel bookBuddy)
        {
            dbContext.BookBuddies.Update(bookBuddy);

            try
            {
                await dbContext.SaveChangesAsync();
                return bookBuddy.Id;
            }

            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx && (sqlEx.Number == 2627 || sqlEx.Number == 2601))
            {
                // 2627: Violation of PRIMARY KEY constraint
                // 2601: Violation of UNIQUE KEY constraint
                if (sqlEx.Message.Contains("IX_BookBuddies_Email"))
                {
                    return -1; // Email constraint violation
                }
                else if (sqlEx.Message.Contains("IX_BookBuddies_Username"))
                {
                    return -2; // Username constraint violation
                }
                throw; // Rethrow if it's not a known unique constraint violation
            }

        }

        public async Task<int> Delete(BookbuddyModel bookBuddy)
        {
            dbContext.BookBuddies.Remove(bookBuddy);
            return await dbContext.SaveChangesAsync();
        }
    }
}
