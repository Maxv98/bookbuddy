using Logic.Handlers;
using Logic.Entities;
using Moq;
using Interfaces.Models;
using Interfaces.Repos;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Unit_Tests.Posts
{
    public class PostHandlerTests
    {
        private readonly PostHandler _handler;
        private readonly Mock<IPostRepo> _mockRepo;

        public PostHandlerTests()
        {
            _mockRepo = new Mock<IPostRepo>();

            // Setup mock behaviors
            _mockRepo.Setup(repo => repo.Add(It.IsAny<PostModel>())).Returns(Task.CompletedTask);
            _mockRepo.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync(new PostModel { Id = 1, Title = "Test Title", Content = "Test Content" });
            _mockRepo.Setup(repo => repo.GetAll(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new List<PostModel>
            {
                new PostModel { Id = 1, Title = "Test Title 1", Content = "Test Content 1" },
                new PostModel { Id = 2, Title = "Test Title 2", Content = "Test Content 2" }
            });
            _mockRepo.Setup(repo => repo.GetPostsByBookbuddy(It.IsAny<int>())).ReturnsAsync(new List<PostModel>
            {
                new PostModel { Id = 1, Title = "Test Title 1", Content = "Test Content 1" }
            });
            _mockRepo.Setup(repo => repo.Delete(It.IsAny<PostModel>())).Returns(Task.CompletedTask);

            _handler = new PostHandler(_mockRepo.Object);
        }

        [Fact]
        public async Task Add_ShouldCallAddOnRepo()
        {
            // Arrange
            Post post = new Post(new PostModel { Id = 1, Title = "Test Title", Content = "Test Content" });

            // Act
            await _handler.Add(post);

            // Assert
            _mockRepo.Verify(repo => repo.Add(It.IsAny<PostModel>()), Times.Once);
        }

        [Fact]
        public async Task Get_ShouldReturnPost_WhenIdExists()
        {
            // Arrange
            int id = 1;

            // Act
            Post result = await _handler.Get(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
            Assert.Equal("Test Title", result.Title);
            Assert.Equal("Test Content", result.Content);
        }

        [Fact]
        public async Task GetAll_ShouldReturnListOfPosts()
        {
            // Arrange
            int limit = 10;
            int page = 1;

            // Act
            List<Post> result = await _handler.GetAll(limit, page);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("Test Title 1", result[0].Title);
            Assert.Equal("Test Title 2", result[1].Title);
        }

        [Fact]
        public async Task GetPostsByBookbuddy_ShouldReturnListOfPosts()
        {
            // Arrange
            int bookbuddyId = 1;

            // Act
            List<Post> result = await _handler.GetPostsByBookbuddy(bookbuddyId);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("Test Title 1", result[0].Title);
        }

        [Fact]
        public async Task Delete_ShouldCallDeleteOnRepo()
        {
            // Arrange
            Post post = new Post(new PostModel { Id = 1, Title = "Test Title", Content = "Test Content" });

            // Act
            await _handler.Delete(post);

            // Assert
            _mockRepo.Verify(repo => repo.Delete(It.IsAny<PostModel>()), Times.Once);
        }
    }
}
