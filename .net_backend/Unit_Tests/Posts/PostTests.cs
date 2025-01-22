using Logic.Entities;
using Interfaces.Models;

namespace Unit_Tests.Posts
{
    public class PostTests
    {
        [Fact]
        public void Constructor_ShouldInitializeProperties()
        {
            // Arrange
            var model = new PostModel
            {
                Id = 1,
                Title = "Test Title",
                Text = "Test Text",
                BookbuddyId = 123
            };

            // Act
            var post = new Post(model);

            // Assert
            Assert.Equal(1, post.Id);
            Assert.Equal("Test Title", post.Title);
            Assert.Equal("Test Text", post.Text);
            Assert.Equal(123, post.BookbuddyId);
        }

        [Fact]
        public void ToModel_ShouldReturnPostModel()
        {
            // Arrange
            var post = new Post(new PostModel
            {
                Id = 1,
                Title = "Test Title",
                Text = "Test Text",
                BookbuddyId = 123
            });

            // Act
            var model = post.ToModel();

            // Assert
            Assert.Equal(1, model.Id);
            Assert.Equal("Test Title", model.Title);
            Assert.Equal("Test Text", model.Text);
            Assert.Equal(123, model.BookbuddyId);
        }

        [Fact]
        public void DefaultConstructor_ShouldInitializePropertiesToDefaultValues()
        {
            // Act
            var post = new Post();

            // Assert
            Assert.Equal(0, post.Id);
            Assert.Null(post.Title);
            Assert.Null(post.Text);
            Assert.Equal(0, post.BookbuddyId);
        }
    }
}

