using Logic.Handlers;
using Logic.Entities;
using Moq;
using Interfaces.Models;
using Interfaces.Repos;
using System.Reflection;
namespace Unit_Tests.Bookbuddies
{
    public class BookbuddyTests
    {
        [Fact]
        public void Test_Bookbuddy_ToModel()
        {
            // Arrange
            Bookbuddy bookbuddy = new Bookbuddy
            {
                Id = 1,
                Email = "email",
                Password = "password",
                Username = "username",
                AboutMe = "about me",
                Interests = "interests"
            };

            // Act
            BookbuddyModel model = bookbuddy.ToModel();

            // Assert
            foreach (PropertyInfo property in typeof(Bookbuddy).GetProperties())
            {
                PropertyInfo? modelProperty = typeof(BookbuddyModel).GetProperty(property.Name);
                if (modelProperty != null)
                {
                    Assert.Equal(property.GetValue(bookbuddy), modelProperty.GetValue(model));
                }
            }
        }

        [Fact]
        public void Test_Model_ToBookbuddy()
        {
            // Arrange
            BookbuddyModel model = new BookbuddyModel
            {
                Id = 1,
                Email = "email",
                Password = "password",
                Username = "username",
                AboutMe = "about me",
                Interests = "interests"
            };

            // Act
            Bookbuddy bookbuddy = new Bookbuddy(model);

            // Assert
            foreach (PropertyInfo property in typeof(Bookbuddy).GetProperties())
            {
                PropertyInfo? modelProperty = typeof(BookbuddyModel).GetProperty(property.Name);
                if (modelProperty != null)
                {
                    Assert.Equal(property.GetValue(bookbuddy), modelProperty.GetValue(model));
                }
            }
        }
    }
}
