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
            };

            // Act
            var bookbuddy = new Bookbuddy(model);

            // Assert
            Assert.Equal(1, bookbuddy.Id);
            Assert.Equal("test@example.com", bookbuddy.Email);
            Assert.Equal("password", bookbuddy.Password);
            Assert.Equal("username", bookbuddy.Username);
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
            });

            // Act
            var model = bookbuddy.ToModel();

            // Assert
            Assert.Equal(1, model.Id);
            Assert.Equal("test@example.com", model.Email);
            Assert.Equal("password", model.Password);
            Assert.Equal("username", model.Username);
        }

        [Fact]
        public void DefaultConstructor_ShouldInitializePropertiesToDefaultValues()
        {
            // Act
            var bookbuddy = new Bookbuddy();

            // Assert
            Assert.Equal(0, bookbuddy.Id);
            Assert.Null(bookbuddy.Email);
            Assert.Null(bookbuddy.Password);
            Assert.Null(bookbuddy.Username);
        }

        [Fact]
        public void ParameterizedConstructor_ShouldInitializeProperties()
        {
            // Arrange
            string email = "test@example.com";
            string password = "password";
            string username = "username";

            // Act
            var bookbuddy = new Bookbuddy(email, password, username);

            // Assert
            Assert.Equal(email, bookbuddy.Email);
            Assert.Equal(password, bookbuddy.Password);
            Assert.Equal(username, bookbuddy.Username);
        }
    }
}
