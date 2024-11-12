using DataAccessLayer;
using DataAccessLayer.Repositories;
using Interfaces.Models;
using Logic.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unit_Tests
{
    public static class BookbuddyTestHelper
    {
        public static BookbuddyRepo GetMockRepo()
        {
            var builder = new DbContextOptionsBuilder<BookbuddyContext>();
            builder.UseInMemoryDatabase("LibraryDbInMemory");

            var dbContextOptions = builder.Options;
            BookbuddyContext bookbuddyContext = new BookbuddyContext(dbContextOptions, useInMemoryDatabase: true);
            // Delete existing db before creating a new one
            bookbuddyContext.Database.EnsureDeleted();
            bookbuddyContext.Database.EnsureCreated();
            return new BookbuddyRepo(bookbuddyContext);
        }

        public static List<Bookbuddy> GetBookbuddies()
        {
            return new List<Bookbuddy>
            {
                new Bookbuddy { Id = 1, Email = "mail1", Username = "user1", Password = "password1" },
                new Bookbuddy { Id = 2, Email = "mail2", Username = "user2", Password = "password2" },
                new Bookbuddy { Id = 3, Email = "mail3", Username = "user3", Password = "password3" }
            };
        }
    }
}
