using Logic.Entities;
using Xunit;

namespace Unit_Tests.Entities
{
    public class SavePostRequestTests
    {
        [Fact]
        public void Constructor_ShouldInitializeProperties()
        {
            // Arrange
            var username = "testuser";
            var postId = 123;

            // Act
            var savePostRequest = new SavePostRequest
            {
                Username = username,
                PostId = postId
            };

            // Assert
            Assert.Equal(username, savePostRequest.Username);
            Assert.Equal(postId, savePostRequest.PostId);
        }

        [Fact]
        public void Username_ShouldGetAndSet()
        {
            // Arrange
            var savePostRequest = new SavePostRequest();
            var username = "testuser";

            // Act
            savePostRequest.Username = username;

            // Assert
            Assert.Equal(username, savePostRequest.Username);
        }

        [Fact]
        public void PostId_ShouldGetAndSet()
        {
            // Arrange
            var savePostRequest = new SavePostRequest();
            var postId = 123;

            // Act
            savePostRequest.PostId = postId;

            // Assert
            Assert.Equal(postId, savePostRequest.PostId);
        }
    }
}

