using Interfaces.Exceptions;
using Interfaces.Models;
using Interfaces.Repos;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories
{
    public class PostRepo(BookbuddyContext dbContext) : IPostRepo
    {
        public async Task<int> Add(PostModel post)
        {
            dbContext.Posts.Add(post);

            try
            {
                await dbContext.SaveChangesAsync();
                return post.Id;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the post.", ex);
            }
        }

        public async Task<PostModel> Get(int id)
        {
            PostModel? post = await dbContext.Posts.FindAsync(id);
            if (post != null)
            {
                return post!;
            }
            else
            {
                throw new NotFoundException("Post not found");
            }
        }

        public async Task<List<PostModel>> GetAll(int limit, int page)
        {
            return await dbContext.Posts.Skip((page - 1) * limit).Take(limit).ToListAsync();
        }

        public async Task<List<PostModel>> GetPostsByBookbuddy(int bookbuddyId)
        {
            return await dbContext.Posts.Where(p => p.BookbuddyId == bookbuddyId).ToListAsync();
        }

        public async Task<bool> Delete(PostModel post)
        {
            dbContext.Posts.Remove(post);
            return await dbContext.SaveChangesAsync() == 1;
        }
    }
}
