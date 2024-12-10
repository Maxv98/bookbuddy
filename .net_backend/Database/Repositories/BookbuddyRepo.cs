using Interfaces.Models;
using Interfaces.Repos;
using Interfaces.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories
{
    public class BookbuddyRepo(BookbuddyContext dbContext) : IBookbuddyRepo
    {
        public async Task<int> Add(BookbuddyModel bookbuddy)
        {
            dbContext.Bookbuddies.Add(bookbuddy);

            try
            {
                await dbContext.SaveChangesAsync();
                return bookbuddy.Id;
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

        public async Task<BookbuddyModel> Get(int id)
        {
            var bookbuddy = await dbContext.Bookbuddies.FindAsync(id);
            if (bookbuddy == null)
            {
                throw new NotFoundException("Bookbuddy not found");
            }
            return bookbuddy;
        }

        public async Task<List<BookbuddyModel>> GetAll()
        {
            return await dbContext.Bookbuddies.ToListAsync();
        }

        public async Task<int> Update(BookbuddyModel bookbuddy)
        {
            dbContext.Bookbuddies.Update(bookbuddy);

            try
            {
                await dbContext.SaveChangesAsync();
                return bookbuddy.Id;
            }

            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx && (sqlEx.Number == 2627 || sqlEx.Number == 2601))
            {
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

        public async Task<bool> Delete(BookbuddyModel bookbuddy)
        {
            dbContext.Bookbuddies.Remove(bookbuddy);
            return await dbContext.SaveChangesAsync() == 1;
        }
    }
}
