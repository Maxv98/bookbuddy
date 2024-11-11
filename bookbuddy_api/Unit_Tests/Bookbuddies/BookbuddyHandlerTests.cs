using DataAccessLayer.Repositories;
using DataAccessLayer;
using Interfaces.Models;
using Logic.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Entities;

namespace Unit_Tests.Bookbuddies
{
    [Collection("Non-Parallel Collection")]
    public class BookbuddyHandlerTests
    {
        private readonly BookbuddyContext context;
        private readonly BookbuddyRepo repo;
        BookbuddyHandler handler;

        public BookbuddyHandlerTests()
        {
            context = TestHelper.GetMockContext();
            repo = new BookbuddyRepo(context);
            handler = new BookbuddyHandler(repo);
            foreach (Bookbuddy bookbuddy in TestHelper.GetBookbuddies())
            {
                Task<int> t = handler.Add(bookbuddy);
            }
        }

        [Fact]
        public async Task AddBookbuddy_ShouldReturnId_WhenBookbuddyIsUnique()
        {
            // Arrange
            BookbuddyHandler handler = new BookbuddyHandler(repo);
            Bookbuddy newBookbuddy = new Bookbuddy
            {
                Id = 5,
                Email = "uniqueMail",
                Username = "uniqueUser",
                Password = "password"
            };
            int result = await handler.Add(newBookbuddy);
            Assert.Equal(5, result);
        }

        [Fact]
        public async Task AddBookbuddy_ShouldReturnMinusOne_WhenEmailIsNotUnique()
        {
            // Arrange
            BookbuddyHandler handler = new BookbuddyHandler(repo);
            Bookbuddy newBookbuddy = new Bookbuddy
            {
                Id = 5,
                Email = "mail1",
                Username = "uniqueUser",
                Password = "password"
            };
            int result = await handler.Add(newBookbuddy);
            Assert.Equal(-1, result);
        }

        [Fact]
        public async Task AddBookbuddy_ShouldReturnMinusTwo_WhenUsernameIsNotUnique()
        {
            // Arrange
            BookbuddyHandler handler = new BookbuddyHandler(repo);
            Bookbuddy newBookbuddy = new Bookbuddy
            {
                Id = 5,
                Email = "uniqueMail",
                Username = "user1",
                Password = "password"
            };
            int result = await handler.Add(newBookbuddy);
            Assert.Equal(-2, result);
        }
    }
}
