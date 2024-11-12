using Logic.Handlers;
using Logic.Entities;

namespace Unit_Tests.Bookbuddies
{
    [Collection("Non-Parallel Collection")]
    public class BookbuddyHandlerTests
    {
        private readonly BookbuddyHandler handler;

        public BookbuddyHandlerTests()
        {
            handler = new BookbuddyHandler(BookbuddyTestHelper.GetMockRepo());
            foreach (Bookbuddy bookbuddy in BookbuddyTestHelper.GetBookbuddies())
            {
                Task<int> t = handler.Add(bookbuddy);
            }
        }

        [Fact]
        public async Task AddBookbuddy_ShouldReturnId_WhenBookbuddyIsUnique()
        {
            // Arrange
            Bookbuddy newBookbuddy = new Bookbuddy
            {
                Id = 5,
                Email = "uniqueMail",
                Username = "uniqueUser",
                Password = "password"
            };
            
            //Act
            int result = await handler.Add(newBookbuddy);

            //Assert
            Assert.Equal(5, result);
        }

        [Fact]
        public async Task AddBookbuddy_ShouldReturnMinusOne_WhenEmailIsNotUnique()
        {
            // Arrange
            Bookbuddy newBookbuddy = new Bookbuddy
            {
                Id = 5,
                Email = "mail1",
                Username = "uniqueUser",
                Password = "password"
            };

            //Act
            int result = await handler.Add(newBookbuddy);

            //Assert
            Assert.Equal(-1, result);
        }

        [Fact]
        public async Task AddBookbuddy_ShouldReturnMinusTwo_WhenUsernameIsNotUnique()
        {
            // Arrange
            Bookbuddy newBookbuddy = new Bookbuddy
            {
                Id = 5,
                Email = "uniqueMail",
                Username = "user1",
                Password = "password"
            };

            //Act
            int result = await handler.Add(newBookbuddy);

            //Assert
            Assert.Equal(-2, result);
        }
    }
}
