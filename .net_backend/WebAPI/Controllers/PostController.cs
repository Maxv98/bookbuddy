using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer;
using Logic.Entities;
using Logic.Handlers;
using Interfaces.Exceptions;

namespace WebAPI.Controllers
{
    [Route("Posts")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly PostHandler _postHandler;

        public PostController(PostHandler postHandler)
        {
            _postHandler = postHandler;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult<Post>> Create([FromBody] Post post)
        {
            try
            {
                int id = await _postHandler.Add(post);
                post.Id = id;
                await WebSocketHandler.NotifyClientsWithPostAsync(post);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("Index")]
        public async Task<ActionResult<List<Post>>> GetAllPosts(int limit, int page)
        {
            try
            {
                List<Post> posts = await _postHandler.GetAll(limit, page);
                return Ok(posts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("ByBookbuddy/{bookbuddyId}")]
        public async Task<ActionResult<List<Post>>> GetPostsByBookbuddy(int bookbuddyId)
        {
            try
            {
                List<Post> posts = await _postHandler.GetPostsByBookbuddy(bookbuddyId);
                return Ok(posts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("SavedByBookbuddy/{bookbuddyId}")]
        public async Task<ActionResult<List<Post>>> GetPostsSavedByBookbuddy(int bookbuddyId)
        {
            try
            {
                List<Post> posts = await _postHandler.GetPostsSavedByBookbuddy(bookbuddyId);
                return Ok(posts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/PostModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> Get(int id)
        {
            try
            {
                Post post = await _postHandler.Get(id);
                return Ok(post);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Post post = await _postHandler.Get(id);

                await _postHandler.Delete(post);
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
