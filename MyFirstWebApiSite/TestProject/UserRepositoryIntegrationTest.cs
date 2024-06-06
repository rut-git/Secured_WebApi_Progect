using Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class UserRepositoryIntegrationTest : IClassFixture<DatabaseFixture>
    {
        private readonly AdoNetUsers326077351Context _dbContext;
        private readonly UsersRepository _userRepository;

        public UserRepositoryIntegrationTest(DatabaseFixture databaseFixture)
        {
            _dbContext = databaseFixture.Context;
            _userRepository = new UsersRepository(_dbContext);
        }

        [Fact]
        public async Task GetById_ExistingUserId_ReturnsUser()
        {
            // Arrange
            var user = new User { Firstname = "Test", Lastname = "Test", Email = "test2@example.com", Password = "password" };
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            int userId = user.UserId;

            // Act
            var result = await _userRepository.GetById(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userId, result.UserId);
        }

        [Fact]
        public async Task Register_ValidUser_SavesAndReturnsUser()
        {
            // Arrange
            var user = new User { Firstname = "New", Lastname = "User", Email = "newuser@example.com", Password = "password" };

            // Act
            var result = await _userRepository.Register(user);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.Email, result.Email);
            Assert.Equal(user.Firstname, result.Firstname);
        }

        [Fact]
        public async Task Login_ValidCredentials_ReturnsUser()
        {
            // Arrange
            var email = "login@ex.com";
            var password = "password";
            User user = new User { Firstname = "Login", Lastname = "User", Email = email, Password = password };
            var userLogin = new UserLogin { Email = email, Password = password };
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _userRepository.Login(userLogin);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.Email, result.Email);
        }

        //[Fact]
        //public async Task Update_ValidIdAndUser_UpdatesAndReturnsUser()
        //{
        //    // Arrange
        //    var user = new User { Firstname = "Update", Lastname = "User", Email = "update@ex.com", Password = "password" };
        //    await _dbContext.Users.AddAsync(user);
        //    await _dbContext.SaveChangesAsync();
        //    int userId = user.UserId;

        //    var updatedUser = new User { Firstname = "Updated", Lastname = "User", Email = "updated@ex.com", Password = "npassword" };

        //    // Act
        //    var result = await _userRepository.Update(userId, updatedUser);

        //    // Assert
        //    Assert.NotNull(result);
        //    Assert.Equal(updatedUser.Firstname, result.Firstname);
        //    Assert.Equal(updatedUser.Email, result.Email);
        //}


        //[Fact]
        //public async Task Update_ValidIdAndUser_UpdatesAndReturnsUser()
        //{
        //    // Arrange
        //    var existingUser = new User { Firstname = "Original", Lastname = "User", Email = "original@ex.com", Password = "password" };
        //    await _dbContext.Users.AddAsync(existingUser);
        //    await _dbContext.SaveChangesAsync();
        //    int userId = existingUser.UserId;

        //    var updatedUser = new User { Firstname = "Updated", Lastname = "User", Email = "updated@ex.com", Password = "password" };

        //    // Act
        //    var result = await _userRepository.Update(userId, updatedUser);

        //    // Assert
        //    Assert.NotNull(result);
        //    Assert.Equal(userId, result.UserId); // Ensure the ID remains the same
        //    Assert.Equal(updatedUser.Firstname, result.Firstname);
        //    Assert.Equal(updatedUser.Lastname, result.Lastname);
        //    Assert.Equal(updatedUser.Email, result.Email);
        //    Assert.Equal(updatedUser.Password, result.Password);

        //    var userInDb = await _dbContext.Users.FindAsync(userId);
        //    Assert.NotNull(userInDb);
        //    Assert.Equal(updatedUser.Firstname, userInDb.Firstname);
        //    Assert.Equal(updatedUser.Lastname, userInDb.Lastname);
        //    Assert.Equal(updatedUser.Email, userInDb.Email);
        //    Assert.Equal(updatedUser.Password, userInDb.Password);
        //}


        [Fact]
        public async Task UpdateUser_ValidId_UpdatesUser()
        {
            // Arrange
            var originalUser = new User { Email = "test@example.com", Password = "Password", Firstname = "Original", Lastname = "User" };
            await _dbContext.Users.AddAsync(originalUser);
            await _dbContext.SaveChangesAsync();
            var userId = originalUser.UserId;

            // Detach the original user to simulate detached state
            _dbContext.Entry(originalUser).State = EntityState.Detached;

            var updatedUser = new User { UserId = userId, Email = "updated@example.com", Password = "NewPass", Firstname = "Updated", Lastname = "User" };

            // Act
            var result = await _userRepository.Update(userId, updatedUser);

            // Reload the user from the database to confirm changes
            var reloadedUser = await _dbContext.Users.FindAsync(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userId, result.UserId);  // Ensure the user's ID remains the same
            Assert.Equal(updatedUser.Email, result.Email);
            Assert.Equal(updatedUser.Password, result.Password);
            Assert.Equal(updatedUser.Firstname, result.Firstname);
            Assert.Equal(updatedUser.Lastname, result.Lastname);

            // Confirm changes in the database
            Assert.Equal(updatedUser.Email, reloadedUser.Email);
            Assert.Equal(updatedUser.Password, reloadedUser.Password);
            Assert.Equal(updatedUser.Firstname, reloadedUser.Firstname);
            Assert.Equal(updatedUser.Lastname, reloadedUser.Lastname);

            // Clean up
            _dbContext.Users.Remove(reloadedUser);
            await _dbContext.SaveChangesAsync();
        }

    }
}

