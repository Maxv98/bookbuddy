using Interfaces.Models;
using Logic.Containers;
using Logic.Entities;

namespace UnitTesting.BookBuddies
{

    [TestClass]
    public class BookBuddyContainerTests
    {
        private Bookbuddy _bookBuddy = null!;
        private BookBuddyContainer _bookBuddyContainer = null!;

        [TestInitialize]
        public void Setup()
        {
            _bookBuddyContainer = new BookBuddyContainer();
            _bookBuddy = new Bookbuddy
            {
                Email = "email",
                Password = "password",
                Username = "username"
            };
        }

        [TestMethod]
        public void ValidateLogin_ValidCredentials_ReturnsTrue()
        {
            // Arrange
            string password = "password";

            // Act
            bool result = BookBuddyContainer.ValidateLogin(_bookBuddy, password);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ValidateLogin_InvalidCredentials_ReturnsFalse()
        {
            // Arrange
            string password = "wrongpassword";

            // Act
            bool result = BookBuddyContainer.ValidateLogin(_bookBuddy, password);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
