using Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Repos
{
    public interface IPostRepo
    {
        Task<int> Add(PostModel post);
        Task<bool> Delete(PostModel post);
        Task<PostModel> Get(int id);
        Task<List<PostModel>> GetAll(int limit, int page);
        Task<List<PostModel>> GetPostsByBookbuddy(int bookbuddyId);
    }
}
