using Interfaces.Models;
using Interfaces.Repos;
using Logic.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Handlers
{
    public class PostHandler
    {
        private readonly IPostRepo _postRepo;

        public PostHandler(IPostRepo postRepo)
        {
            _postRepo = postRepo;
        }

        public async Task<int> Add(Post post)
        {
            return await _postRepo.Add(post.ToModel());
        }

        public async Task<Post> Get(int postId)
        {
            PostModel model = await _postRepo.Get(postId);
            return new Post(model);
        }

        public async Task<List<Post>> GetAll(int limit, int page)
        {
            List<PostModel> models = await _postRepo.GetAll(limit, page);
            return models.Select(model => new Post(model)).ToList();
        }

        public async Task<List<Post>> GetPostsByBookbuddy(int bookbuddyId)
        {
            List<PostModel> models = await _postRepo.GetPostsByBookbuddy(bookbuddyId);
            return models.Select(model => new Post(model)).ToList();
        }

        public async Task<List<Post>> GetPostsSavedByBookbuddy(int bookbuddyId)
        {
            List<PostModel> models = await _postRepo.GetPostsSavedByBookbuddy(bookbuddyId);
            return models.Select(model => new Post(model)).ToList();
        }


        public async Task<bool> Delete(Post post)
        {
            PostModel model = post.ToModel();
            return await _postRepo.Delete(model);
        }
    }
}
