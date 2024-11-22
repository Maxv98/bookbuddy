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
        public async Task<IActionResult> CreateBookbuddy([FromBody] Bookbuddy bookbuddy)
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
                Bookbuddy? bookBuddy = await _bookbuddyHandler.Get(id);
                return Ok(bookBuddy);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
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
    }
}
