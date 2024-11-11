using Interfaces.Models;
using Interfaces.Repos;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories
{
    public class BookbuddyRepo
        (BookbuddyContext dbContext) : IBookbuddyRepo
    {
        /// <summary>
        /// Checks if the email or username of the given BookBuddyModel is unique.
        /// </summary>
        /// <param name="bookBuddy">The BookBuddyModel to check.</param>
        /// <returns>0 if okay, -1 if email constraint, -2 if username constraint</returns>
        private async Task<int> CheckUniqueConstraints(BookbuddyModel bookBuddy)
        {
            int output = 0;
            if (await dbContext.BookBuddies.AnyAsync(b => b.Email == bookBuddy.Email))
            {
                output = -1;
            }
            else if (await dbContext.BookBuddies.AnyAsync(b => b.Username == bookBuddy.Username))
            {
                output = -2;
            }

            return output;
        }

        public async Task<int> Add(BookbuddyModel bookBuddy)
        {
            int output = await CheckUniqueConstraints(bookBuddy);

            if (output == 0)
            {
                dbContext.BookBuddies.Add(bookBuddy);
                await dbContext.SaveChangesAsync();
                output = bookBuddy.Id;
            }

            return output;
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
                return await dbContext.SaveChangesAsync();

            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx && sqlEx.Number == 2627)
            {
                throw;
            }
        }

        public async Task<int> Delete(BookbuddyModel bookBuddy)
        {
            dbContext.BookBuddies.Remove(bookBuddy);
            return await dbContext.SaveChangesAsync();
        }
    }
}
