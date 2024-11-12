using DataAccessLayer;
using DataAccessLayer.Repositories;
using Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unit_Tests.Bookbuddies
{
    [Collection("Non-Parallel Collection")]
    public class BookbuddyRepoTests
    {
        private readonly BookbuddyContext context;
        private readonly BookbuddyRepo repo;

        public BookbuddyRepoTests()
        {
            context = TestHelper.GetMockContext();
            repo = new BookbuddyRepo(context);
            foreach (BookbuddyModel bookbuddy in TestHelper.GetBookbuddyModels())
            {
                Task<int> t = repo.Add(bookbuddy);
            }
        }

        [Fact]
        public async Task Add_ShouldReturnId_WhenBookbuddyIsUnique()
        {
            // Arrange
            BookbuddyModel newBookbuddy = new BookbuddyModel { Id = 5, Email = "unique@mail.com", Username = "uniqueUser", Password = "password" };

            // Act
            int result = await repo.Add(newBookbuddy);

            // Assert
            Assert.Equal(5, result);
        }

        [Fact]
        public async Task Add_ShouldReturnMinusOne_WhenEmailIsNotUnique()
        {
            // Arrange
            BookbuddyModel newBookbuddy = new BookbuddyModel { Id = 5, Email = "mail1", Username = "uniqueUser", Password = "password" };

            // Act
            int result = await repo.Add(newBookbuddy);

            // Assert
            Assert.Equal(-1, result);
        }

        [Fact]
        public async Task Add_ShouldReturnMinusTwo_WhenUsernameIsNotUnique()
        {
            // Arrange
            BookbuddyModel newBookbuddy = new BookbuddyModel { Id = 5, Email = "unique@mail.com", Username = "user1", Password = "password" };

            // Act
            int result = await repo.Add(newBookbuddy);

            // Assert
            Assert.Equal(-2, result);
        }
    }
}
