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
            _mockRepo.Setup(repo => repo.Add(It.IsAny<PostModel>())).ReturnsAsync(1);
            _mockRepo.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync(new PostModel { Id = 1, Title = "Test Title", Text = "Test Content" });
            _mockRepo.Setup(repo => repo.GetAll(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new List<PostModel>
                {
                    new PostModel { Id = 1, Title = "Test Title 1", Text = "Test Content 1" },
                    new PostModel { Id = 2, Title = "Test Title 2", Text = "Test Content 2" }
                });
            _mockRepo.Setup(repo => repo.GetPostsByBookbuddy(It.IsAny<int>())).ReturnsAsync(new List<PostModel>
                {
                    new PostModel { Id = 1, Title = "Test Title 1", Text = "Test Content 1" }
                });
            _mockRepo.Setup(repo => repo.GetPostsSavedByBookbuddy(It.IsAny<int>())).ReturnsAsync(new List<PostModel>
                {
                    new PostModel { Id = 1, Title = "Saved Title 1", Text = "Saved Content 1" },
                    new PostModel { Id = 2, Title = "Saved Title 2", Text = "Saved Content 2" }
                });
            _mockRepo.Setup(repo => repo.Delete(It.IsAny<PostModel>())).ReturnsAsync(true);

            _handler = new PostHandler(_mockRepo.Object);
        }

        [Fact]
        public async Task Add_ShouldReturnId()
        {
            // Arrange
            Post post = new Post(new PostModel { Id = 1, Title = "Test Title", Text = "Test Content" });

            // Act
            int result = await _handler.Add(post);

            // Assert
            Assert.Equal(1, result);
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
            Assert.Equal("Test Content", result.Text);
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
        public async Task GetPostsSavedByBookbuddy_ShouldReturnListOfPosts()
        {
            // Arrange
            int bookbuddyId = 1;

            // Act
            List<Post> result = await _handler.GetPostsSavedByBookbuddy(bookbuddyId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("Saved Title 1", result[0].Title);
            Assert.Equal("Saved Title 2", result[1].Title);
        }

        [Fact]
        public async Task Delete_ShouldCallDeleteOnRepo()
        {
            // Arrange
            Post post = new Post(new PostModel { Id = 1, Title = "Test Title", Text = "Test Content" });

            // Act
            await _handler.Delete(post);

            // Assert
            _mockRepo.Verify(repo => repo.Delete(It.IsAny<PostModel>()), Times.Once);
        }
    }
}

