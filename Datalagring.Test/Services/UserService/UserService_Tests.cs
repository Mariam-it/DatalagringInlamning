
using DbTest.Services;
using DbTest.Contexts;
using DbTest.Entities;
using DbTest.Repositories;
using DbTest.ViewModels;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Datalagring.Test.Services.UserService;

public class UserService_Tests
{
    private readonly DataContext _context = new(new DbContextOptionsBuilder<DataContext>()
    .UseInMemoryDatabase($"{Guid.NewGuid()}")
    .Options);

    [Fact]
    public async Task CreateUser_ReturnsTrue_WhenUserIsCreated()
    {
        // Arrange
        var userRepository = new UserRepository(_context);
        var userService = new DbTest.Services.UserService(_context, userRepository);


        var userViewModel = new UserViewModel
        {
            Email = "test@test.com",
            Password = "password",
            FirstName = "Test",
            LastName = "User",
            StreetName = "streetname",
            PostalCode = "4521",
            City = "city",
            RoleName = "role"
        };


        // Act
        var result = await userService.CreateUser(userViewModel);

        // Assert
        Assert.True(result);

        // Ytterligare validering: Kontrollera att användaren faktiskt har skapats i databasen
        var userInDb = await _context.Users.FirstOrDefaultAsync(u => u.Email == userViewModel.Email);
        Assert.NotNull(userInDb);
        Assert.Equal(userViewModel.Email, userInDb.Email);
    }

    [Fact]
    public async Task GetUserByEmail_ReturnsCorrectUser_WhenEmailExists()
    {
        // Arrange
        var userRepository = new UserRepository(_context);
        var userService = new DbTest.Services.UserService(_context, userRepository);
        var testEmail = "testuser@example.com";
        var testUser = new UserEntity
        {
            Email = testEmail,
            Password = "testpassword",
            Profile = new ProfileEntity { FirstName = "New", LastName = "User" },
            Address = new AddressEntity { StreetAddress = "New Street", City = "New City", PostalCode = "New Postal" },
            Role = new RoleEntity { RoleName = "NewRole" }
        };

        await _context.Users.AddAsync(testUser);
        await _context.SaveChangesAsync();

        // Act
        var resultUser = await userService.GetUserByEmail(testEmail);

        // Assert
        Assert.NotNull(resultUser);
        Assert.Equal(testEmail, resultUser.Email);
    }

    [Fact]
    public async Task GetUserByEmail_ReturnsNull_WhenEmailDoesNotExist()
    {
        // Arrange
        var userRepository = new UserRepository(_context);
        var userService = new DbTest.Services.UserService(_context, userRepository);
        var nonExistentEmail = "nonexistent@example.com";

        // Act
        var resultUser = await userService.GetUserByEmail(nonExistentEmail);

        // Assert
        Assert.Null(resultUser);
    }

    [Fact]
    public async Task GetUsers_ReturnsAllUsers_WhenUsersExist()
    {
        // Arrange
        var userRepository = new UserRepository(_context);
        var userService = new DbTest.Services.UserService(_context, userRepository);

        // Skapa och lägg till användare i databasen
        var usersToAdd = new List<UserEntity>
            {
                new() { Email = "user1@example.com", Password = "Password1",
                    Profile = new ProfileEntity { FirstName = "New", LastName = "User" },
                    Address = new AddressEntity { StreetAddress = "New Street", City = "New City", PostalCode = "New Postal" },
                    Role = new RoleEntity { RoleName = "NewRole" } },
                new() {Email = "user2@example.com", Password = "Password2", 
                    Profile = new ProfileEntity { FirstName = "New2", LastName = "User2" }, 
                    Address = new AddressEntity { StreetAddress = "New Street2", City = "New City2", PostalCode = "New Postal2" }, 
                    Role = new RoleEntity { RoleName = "NewRole2" }}
            };

        foreach (var user in usersToAdd)
        {
            await _context.Users.AddAsync(user);
        }
        await _context.SaveChangesAsync();

        // Act
        var users = await userService.GetUsers();

        // Assert
        Assert.NotNull(users);
        var usersList = users.ToList();
        Assert.Equal(usersToAdd.Count, usersList.Count); // Kontrollera att antalet returnerade användare matchar det förväntade
        foreach (var user in usersToAdd)
        {
            Assert.Contains(usersList, u => u.Email == user.Email); // Kontrollera att varje tillagd användare finns i den returnerade listan
        }
    }

    [Fact]
    public async Task GetUsers_ReturnsEmptyList_WhenNoUsersExist()
    {
        // Arrange
        var userRepository = new UserRepository(_context);
        var userService = new DbTest.Services.UserService(_context, userRepository);

        // Se till att databasen är tom
        _context.Users.RemoveRange(_context.Users);
        await _context.SaveChangesAsync();

        // Act
        var users = await userService.GetUsers();

        // Assert
        Assert.NotNull(users);
        Assert.Empty(users); // Kontrollera att metoden returnerar en tom lista när det inte finns några användare
    }

    [Fact]
    public async Task UpdateUser_UpdatesUserDetails_WhenUserExists()
    {
        // Arrange
        var userRepository = new UserRepository(_context);
        var userService = new DbTest.Services.UserService(_context, userRepository);
        var existingUser = new UserEntity
        {
            Email = "existinguser@example.com",
            Password = "OldPassword",
            Profile = new ProfileEntity { FirstName = "OldFirstName", LastName = "OldLastName" },
            Address = new AddressEntity { StreetAddress = "OldStreet", City = "OldCity", PostalCode = "OldPostal" },
            Role = new RoleEntity { RoleName = "OldRole"}
        };

        await _context.Users.AddAsync(existingUser);
        await _context.SaveChangesAsync();

        var updatedModel = new UserViewModel
        {
            Email = "existinguser@example.com",
            Password = "NewPassword",
            FirstName = "NewFirstName",
            LastName = "NewLastName",
            StreetName = "NewStreet",
            City = "NewCity",
            PostalCode = "NewPostal",
            RoleName = "NewRole"
        };

        // Act
        var resultUser = await userService.UpdateUser(updatedModel, existingUser);

        // Assert
        Assert.NotNull(resultUser);
        Assert.Equal(updatedModel.Email, resultUser.Email);
        Assert.Equal(updatedModel.Password, resultUser.Password);
        Assert.Equal(updatedModel.FirstName, resultUser.Profile.FirstName);
        Assert.Equal(updatedModel.LastName, resultUser.Profile.LastName);
        Assert.Equal(updatedModel.StreetName, resultUser.Address.StreetAddress);
        Assert.Equal(updatedModel.City, resultUser.Address.City);
        Assert.Equal(updatedModel.PostalCode, resultUser.Address.PostalCode);

        var userInDb = await _context.Users.Include(u => u.Profile).Include(u => u.Address).FirstOrDefaultAsync(u => u.Email == updatedModel.Email);
        Assert.NotNull(userInDb);
        Assert.Equal(updatedModel.Password, userInDb.Password); // Kontrollera att lösenordet uppdaterades korrekt
                                                                // Ytterligare kontroller för Profile och Address som behövs
    }

    [Fact]
    public async Task UpdateUser_ReturnsNull_WhenUserDoesNotExist()
    {
        // Arrange
        var userRepository = new Mock<UserRepository>(_context);
        var userService = new DbTest.Services.UserService(_context, userRepository.Object);
        var nonExistentUserModel = new UserViewModel
        {
            Email = "nonexistent@example.com",
            // Övriga fält
        };

        // Act
        var result = await userService.UpdateUser(nonExistentUserModel, null!);

        // Assert
        Assert.Null(result);
    }


}
