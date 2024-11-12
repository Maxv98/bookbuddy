//using Moq;
//using Interfaces.Repos;
//using Logic.Handlers;
//using Logic.Entities;
//using Interfaces.Models;

//namespace UnitTesting.BookBuddies
//{
//    [TestClass]
//    public class BookBuddyHandlerTests
//    {
//        private Mock<IBookbuddyRepo> _mockbookBuddyDal = null!;
//        private BookbuddyHandler _bookBuddyHandler = null!;
//        private Bookbuddy _bookBuddy = null!;

//        [TestInitialize]
//        public void Setup()
//        {
//            _mockbookBuddyDal = new Mock<IBookbuddyRepo>();
//            _bookBuddyHandler = new BookbuddyHandler(_mockbookBuddyDal.Object);
//            _bookBuddy = new Bookbuddy
//            {
//                Email = "email",
//                Password = "password",
//                Username = "username"
//            };
//        }

//        [TestMethod]
//        public async Task AddBookBuddy_ValidBookBuddy_ReturnsId()
//        {
//            // Arrange
//            int expectedId = 1;
//            _mockbookBuddyDal.Setup(x => x.Add(It.IsAny<BookbuddyModel>())).ReturnsAsync(expectedId);

//            // Act
//            int result = await _bookBuddyHandler.AddBookbuddy(_bookBuddy);

//            // Assert
//            Assert.AreEqual(expectedId, result);
//        }

//        [TestMethod]
//        public async Task AddBookBuddy_ExceptionThrown_RethrowsException()
//        {
//            // Arrange
//            _mockbookBuddyDal.Setup(x => x.Add(It.IsAny<BookbuddyModel>())).Throws(new Exception());

//            // Act & Assert
//            await Assert.ThrowsExceptionAsync<Exception>(async () => await _bookBuddyHandler.AddBookbuddy(_bookBuddy));
//        }

//        [TestMethod]
//        public async Task ValidateLogin_ValidCredentials_ReturnsTrue()
//        {
//            // Arrange
//            string email = "email";
//            string password = "password";

//            _mockbookBuddyDal.Setup(x => x.GetByEmail(email)).ReturnsAsync(new BookbuddyModel() { Username = "username", Email = "email", Password = "password" });

//            // Act
//            bool result = await _bookBuddyHandler.ValidateLogin(email, password);

//            // Assert
//            Assert.IsTrue(result);
//        }

//        [TestMethod]
//        public async Task ValidateLogin_InvalidCredentials_ReturnsFalse()
//        {
//            // Arrange
//            string email = "email";
//            string password = "password";
//            _mockbookBuddyDal.Setup(x => x.GetByEmail(email)).ReturnsAsync((BookbuddyModel?)null);

//            // Act
//            bool result = await _bookBuddyHandler.ValidateLogin(email, password);

//            // Assert
//            Assert.IsFalse(result);
//        }

//        [TestMethod]
//        public async Task GetBookBuddy_ExistingId_ReturnsBookBuddy()
//        {
//            // Arrange
//            int id = 1;
//            _mockbookBuddyDal.Setup(x => x.Get(id)).ReturnsAsync(new BookbuddyModel() { Username = "username", Email = "email", Password = "password" });

//            // Act
//            Bookbuddy? result = await _bookBuddyHandler.GetBookBuddy(id);

//            // Assert
//            Assert.AreEqual(result.Email, "email");
//        }

//        [TestMethod]
//        public async Task GetBookBuddy_NonExistingId_ReturnsNull()
//        {
//            // Arrange
//            int id = 1;
//            _mockbookBuddyDal.Setup(x => x.Get(id)).ReturnsAsync((BookbuddyModel?)null);

//            // Act
//            Bookbuddy? result = await _bookBuddyHandler.GetBookBuddy(id);

//            // Assert
//            Assert.IsNull(result);
//        }

//        [TestMethod]
//        public async Task GetAllBookBuddies_ReturnsListOfBookBuddyModels()
//        {
//            // Arrange
//            var expectedBookBuddies = new List<BookbuddyModel>();
//            _mockbookBuddyDal.Setup(x => x.GetAll()).ReturnsAsync(expectedBookBuddies);

//            // Act
//            List<Bookbuddy> result = await _bookBuddyHandler.GetAllBookBuddies();

//            // Assert
//            CollectionAssert.AreEqual(expectedBookBuddies, result);
//        }

//        [TestMethod]
//        public async Task UpdateBookBuddy_ValidBookBuddy_ReturnsId()
//        {
//            // Arrange
//            int expectedId = 1;
//            _mockbookBuddyDal.Setup(x => x.Update(It.IsAny<BookbuddyModel>())).ReturnsAsync(expectedId);

//            // Act
//            int result = await _bookBuddyHandler.UpdateBookBuddy(_bookBuddy);

//            // Assert
//            Assert.AreEqual(expectedId, result);
//        }

//        [TestMethod]
//        public async Task DeleteBookBuddy_ValidBookBuddy_ReturnsId()
//        {
//            // Arrange
//            int expectedId = 1;
//            _mockbookBuddyDal.Setup(x => x.Delete(It.IsAny<BookbuddyModel>())).ReturnsAsync(expectedId);

//            // Act
//            int result = await _bookBuddyHandler.DeleteBookbuddy(_bookBuddy);

//            // Assert
//            Assert.AreEqual(expectedId, result);
//        }
//    }
//}