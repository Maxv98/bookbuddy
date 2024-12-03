using Logic.Handlers;
using Logic.Entities;
using Moq;
using Interfaces.Models;
using Interfaces.Repos;

namespace Unit_Tests.Bookbuddies
{
    public class BookbuddyHandlerTests
    {
        private readonly BookbuddyHandler _handler;

        public BookbuddyHandlerTests()
        {
            Mock<IBookbuddyRepo> mockRepo = new Mock<IBookbuddyRepo>();

            // Setup mock behaviors
            mockRepo.Setup(repo => repo.Add(It.IsAny<BookbuddyModel>())).ReturnsAsync(1);
            mockRepo.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync(new BookbuddyModel { Id = 1, Username = "username", Email = "test@example.com", Password = "password" });
            mockRepo.Setup(repo => repo.Update(It.IsAny<BookbuddyModel>())).ReturnsAsync(1);

            _handler = new BookbuddyHandler(mockRepo.Object);
        }

        [Fact]
        public async Task Get_ShouldReturnBookbuddy_WhenIdExists()
        {
            // Arrange
            int id = 1;

            // Act
            Bookbuddy? result = await _handler.Get(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(id, result!.Id);
            Assert.Equal("username", result.Username);
            Assert.Equal("test@example.com", result.Email);
        }

        [Fact]
        public async Task Get_ShouldReturnNull_WhenIdDoesNotExist()
        {
            // Arrange
            Mock<IBookbuddyRepo> mockRepo = new Mock<IBookbuddyRepo>();
            mockRepo.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync((BookbuddyModel?)null);
            BookbuddyHandler handler = new BookbuddyHandler(mockRepo.Object);
            int id = 69;

            // Act
            Bookbuddy? result = await handler.Get(id);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAll_ShouldReturnListOfBookbuddies()
        {
            // Arrange
            Mock<IBookbuddyRepo> mockRepo = new Mock<IBookbuddyRepo>();
            mockRepo.Setup(repo => repo.GetAll()).ReturnsAsync(new List<BookbuddyModel>
            {
                new BookbuddyModel { Id = 1, Username = "username1", Email = "test1@example.com", Password = "password1" },
                new BookbuddyModel { Id = 2, Username = "username2", Email = "test2@example.com", Password = "password2" }
            });
            BookbuddyHandler handler = new BookbuddyHandler(mockRepo.Object);

            // Act
            List<Bookbuddy> result = await handler.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("username1", result[0].Username);
            Assert.Equal("username2", result[1].Username);
        }

        [Fact]
        public async Task DeleteBookbuddy_ShouldReturnTrue_WhenSuccesful()
        {
            // Arrange
            Mock<IBookbuddyRepo> mockRepo = new Mock<IBookbuddyRepo>();
            mockRepo.Setup(repo => repo.Delete(It.IsAny<BookbuddyModel>())).ReturnsAsync(true);
            BookbuddyHandler handler = new BookbuddyHandler(mockRepo.Object);
            Bookbuddy bookbuddy = new Bookbuddy(new BookbuddyModel { Id = 1, Username = "username", Email = "test@example.com", Password = "password" });

            // Act
            Assert.True(await handler.DeleteBookbuddy(bookbuddy));

        }
    }
}
