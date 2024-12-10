using Logic.Entities;
using Interfaces.Models;
using Xunit;

namespace Unit_Tests.Bookbuddies
{
    public class BookbuddyTests
    {
        [Fact]
        public void Constructor_ShouldInitializeProperties()
        {
            // Arrange
            var model = new BookbuddyModel
            {
                Id = 1,
                Email = "test@example.com",
                Password = "password",
                Username = "username",
                AboutMe = "About me",
                Interests = "Interests"
            };

            // Act
            var bookbuddy = new Bookbuddy(model);

            // Assert
            Assert.Equal(1, bookbuddy.Id);
            Assert.Equal("test@example.com", bookbuddy.Email);
            Assert.Equal("password", bookbuddy.Password);
            Assert.Equal("username", bookbuddy.Username);
            Assert.Equal("About me", bookbuddy.AboutMe);
            Assert.Equal("Interests", bookbuddy.Interests);
        }

        [Fact]
        public void ToModel_ShouldReturnBookbuddyModel()
        {
            // Arrange
            var bookbuddy = new Bookbuddy(new BookbuddyModel
            {
                Id = 1,
                Email = "test@example.com",
                Password = "password",
                Username = "username",
                AboutMe = "About me",
                Interests = "Interests"
            });

            // Act
            var model = bookbuddy.ToModel();

            // Assert
            Assert.Equal(1, model.Id);
            Assert.Equal("test@example.com", model.Email);
            Assert.Equal("password", model.Password);
            Assert.Equal("username", model.Username);
            Assert.Equal("About me", model.AboutMe);
            Assert.Equal("Interests", model.Interests);
        }
    }
}
