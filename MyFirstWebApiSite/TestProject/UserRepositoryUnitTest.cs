using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using Moq.EntityFrameworkCore;
using Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestProject
{
    public class UserRepositoryUnitTest
    {
        [Fact]

        public async Task GetUser_ValidCredentials_ReturnsUser()
        {
            var user = new User { Email = "test@ex.com", Password = "password" };

            var mockContext= new Mock<AdoNetUsers326077351Context>();
            var users = new List<User>() { user };
            mockContext.Setup(x => x.Users).ReturnsDbSet(users);

            var userRepository = new UsersRepository(mockContext.Object);

            var userLogin = new UserLogin { Email = user.Email, Password = user.Password };

            var result = await userRepository.Login(userLogin);

            Assert.Equal(user, result);

        }

        [Fact]
        public async Task GetById_ExistingUser_ReturnsUser()
        {
            // Arrange
            int userId = 1;
            var user = new User { UserId = userId, Email = "test@ex.com", Password = "password" };

            var mockContext = new Mock<AdoNetUsers326077351Context>();
            mockContext.Setup(x => x.Users.FindAsync(userId)).ReturnsAsync(user);

            var userRepository = new UsersRepository(mockContext.Object);

            // Act
            var result = await userRepository.GetById(userId);

            // Assert
            Assert.Equal(user, result);
        }

        [Fact]
        public async Task Register_ValidUser_ReturnsUser()
        {
            // Arrange
            var user = new User { Firstname = "Test", Lastname = "Test", Email = "test2@ex.com", Password = "password" };

            var users = new List<User>();
            var mockContext = new Mock<AdoNetUsers326077351Context>();
            mockContext.Setup(x => x.Users).ReturnsDbSet(users);

            var userRepository = new UsersRepository(mockContext.Object);

            // Act
            var result = await userRepository.Register(user);

            // Assert
            Assert.Equal(user, result);
        }

        //[Fact]
        //public async Task UpdateUser_ValidUser_UpdateUserDetails()
        //{
        //    // Arrange
        //    var id = 0;
        //    var userToUpdate = new User { Email = "test@gmail.com", Firstname = "Updated", Lastname = "Updated" };
        //    var mockContext = new Mock<AdoNetUsers326077351Context>();
        //    var mockDbSet = new Mock<DbSet<User>>();

        //    mockDbSet.Setup(m => m.Update(It.IsAny<User>())).Callback<User>(u =>
        //    {
        //        u.UserId = id;
        //    });

        //    mockContext.Setup(x => x.Users).Returns(mockDbSet.Object);
        //    mockContext.Setup(x => x.Update(It.IsAny<User>())).Callback<User>(u =>
        //    {
        //        u.UserId = id;
        //    });
        //    mockContext.Setup(x => x.SaveChangesAsync(default)).ReturnsAsync(1);

        //    var userRepository = new UsersRepository(mockContext.Object);

        //    // Act
        //    var result = await userRepository.Update(id, userToUpdate);

        //    // Assert
        //    Assert.NotNull(result);
        //    Assert.Equal(id, result.UserId);
        //    Assert.Equal("test@gmail.com", result.Email);
        //    Assert.Equal("Updated", result.Firstname);
        //    Assert.Equal("Updated", result.Lastname);
        //}

        [Fact]
        public async Task UpdateUser_ValidUser_UpdatesUser()
        {
            // Arrange
            var user = new User { Firstname = "original", Lastname = "original", Email = "original@gmail.com", Password = "password" };
            var updatedUser = new User { Firstname = "updated", Lastname = "user", Email = "updated@gmail.com", Password = "newpassword" };

            var mockSet = new Mock<DbSet<User>>();
            var mockContext = new Mock<AdoNetUsers326077351Context>();

            mockSet.Setup(m => m.Update(It.IsAny<User>()));
            mockContext.Setup(m => m.Users).Returns(mockSet.Object);

            var userRepository = new UsersRepository(mockContext.Object);

            // Act
            var result = await userRepository.Update(user.UserId, updatedUser);

            // Assert
            Assert.Equal("updated@gmail.com", result.Email);
        }
    }
}
