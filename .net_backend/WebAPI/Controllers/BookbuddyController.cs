using Microsoft.AspNetCore.Mvc;
using Logic.Handlers;
using Logic.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    [Route("Bookbuddy")]
    [ApiController]
    public class BookbuddyController : Controller
    {
        private readonly BookbuddyHandler _bookbuddyHandler;

        public BookbuddyController(BookbuddyHandler bookbuddyHandler)
        {
            _bookbuddyHandler = bookbuddyHandler;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] Bookbuddy bookbuddy)
        {
            try
            {
                IActionResult actionResult;
                int id = await _bookbuddyHandler.Add(bookbuddy);
                if (id == -1)
                {
                    actionResult = Conflict("This email already exists");
                }
                else if (id == -2)
                {
                    actionResult = Conflict("This username already exists");
                }
                else
                {
                    actionResult = Ok(id);
                }
                return actionResult;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookbuddy(int id)
        {
            try
            {
                Bookbuddy? bookbuddy = await _bookbuddyHandler.Get(id);
                return Ok(bookbuddy);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("Index")]
        public async Task<IActionResult> GetBookbuddies()
        {
            try
            {
                List<Bookbuddy> bookbuddies = await _bookbuddyHandler.GetAll();
                return Ok(bookbuddies);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateBookbuddy([FromBody] Bookbuddy bookbuddy)
        {
            try
            {
                IActionResult actionResult;
                int id = await _bookbuddyHandler.Update(bookbuddy);
                if (id == -1)
                {
                    actionResult = Conflict("This email already exists");
                }
                else if (id == -2)
                {
                    actionResult = Conflict("This username already exists");
                }
                else
                {
                    actionResult = Ok(id);
                }
                return actionResult;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> DeleteBookbuddy([FromBody] Bookbuddy bookbuddy)
        {
            try
            {
                if (await _bookbuddyHandler.DeleteBookbuddy(bookbuddy))
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("SavePost")]
        public async Task<IActionResult> SavePost([FromBody] int bookbuddyId, int postId)
        {
            try
            {
                IActionResult actionResult;
                bool success = await _bookbuddyHandler.SavePost(bookbuddyId, postId);
                if (success)
                {
                    actionResult = Ok();
                }
                else
                {
                    actionResult = BadRequest("Failed to save post. Please try again.");
                }

                return actionResult;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
