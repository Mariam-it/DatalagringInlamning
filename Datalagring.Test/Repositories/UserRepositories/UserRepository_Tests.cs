
using DbTest.Contexts;
using DbTest.Entities;
using DbTest.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Datalagring.Test.Repositories.UserRepositories;

public class UserRepository_Tests
{
    private readonly DataContext _context = new(new DbContextOptionsBuilder<DataContext>()
        .UseInMemoryDatabase($"{Guid.NewGuid()}")
        .Options);

    [Fact]
    public async Task GetAsync_ReturnsExistingUser()
    {
        // Arrange
        var userRepository = new UserRepository(_context);
        var user = new UserEntity { Id = Guid.NewGuid().ToString(), Email = "demo@domain.com", Password = "YourSecurePassword",
            Profile = new ProfileEntity { FirstName = "testname", LastName = "testlastname" },
            Address = new AddressEntity { StreetAddress = "Demoaddtrs", City = "DemoCity", PostalCode = "5465" },
            Role = new RoleEntity { RoleName = "demorole" },
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        // Act
        var result = await userRepository.GetAsync(u => u.Email == user.Email);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("demo@domain.com", result.Email);
        // Du kan lägga till fler kontroller här för att validera andra egenskaper hos användaren
    }
    [Fact]
    public async Task GetAsync_ReturnsNullForNonExistingUser()
    {
        // Arrange
        var userRepository = new UserRepository(_context);
        // Act
        var result = await userRepository.GetAsync(u => u.Id == Guid.NewGuid().ToString());

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAllUsersWithRelatedEntities()
    {
        // Arrange
        var userRepository = new UserRepository(_context);

        // Skapa och lägg till användare med relaterade entiteter
        var users = new List<UserEntity>
    {
        new() { Email = "user1@domain.com", Password = "Password1",
            Profile = new ProfileEntity { FirstName = "FirstName1", LastName = "LastName1" },
            Address = new AddressEntity { StreetAddress = "Address1", City = "City1", PostalCode = "Postal1" },
            Role = new RoleEntity { RoleName = "Role1" } },
        new() { Email = "user2@domain.com", Password = "Password2",
            Profile = new ProfileEntity { FirstName = "FirstName2", LastName = "LastName2" },
            Address = new AddressEntity { StreetAddress = "Address2", City = "City2", PostalCode = "Postal2" },
            Role = new RoleEntity { RoleName = "Role2" } }
    };

        foreach (var user in users)
        {
            _context.Users.Add(user);
        }
        await _context.SaveChangesAsync();

        // Act
        var result = await userRepository.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        foreach (var user in result)
        {
            Assert.NotNull(user.Profile);
            Assert.NotNull(user.Address);
            Assert.NotNull(user.Role);
            // Ytterligare assertions kan läggas till här för att kontrollera detaljer för relaterade entiteter
        }
    }

    [Fact]
    public async Task Updatesync_UpdatesExistingUserAndReturnsUpdatedEntity()
    {
        // Arrange
        var userRepository = new UserRepository(_context);
        var originalUser = new UserEntity
        {
            Email = "original@domain.com",
            Password = "OriginalPassword",
            Profile = new ProfileEntity { FirstName = "OriginalFirstName", LastName = "OriginalLastName" },
            Address = new AddressEntity { StreetAddress = "OriginalAddress", City = "OriginalCity", PostalCode = "OriginalPostal" },
            Role = new RoleEntity { RoleName = "OriginalRole" }
        };

        _context.Users.Add(originalUser);
        await _context.SaveChangesAsync();

        var existinguser = await userRepository.GetAsync(u => u.Email == originalUser.Email);

        // Skapa uppdaterade data
        existinguser.Password = originalUser.Password;
        existinguser.Profile.FirstName = originalUser.Profile.FirstName;
        existinguser.Profile.LastName = originalUser.Profile.LastName;
        existinguser.Address.StreetAddress = originalUser.Address.StreetAddress;
        existinguser.Address.PostalCode = originalUser.Address.PostalCode;
        existinguser.Address.City = originalUser.Address.City;


        // Act
        var result = await userRepository.Updatesync(u => u.Email == originalUser.Email, existinguser);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(existinguser.Email, result.Email);
        Assert.Equal(existinguser.Password, result.Password); 

        // Hämta användaren direkt från databasen för att verifiera att uppdateringen sparades korrekt
        var dbUser = await _context.Users.Include(u => u.Profile).Include(u => u.Address).Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == existinguser.Email);
        Assert.NotNull(dbUser);
        Assert.Equal(existinguser.Email, dbUser.Email);
    }

    [Fact]
    public async Task CreateAsync_AddsNewUserToDatabase()
    {
        // Arrange
        var userRepository = new UserRepository(_context);
        var newUser = new UserEntity
        {
            Email = "newuser@domain.com",
            Password = "NewUserPassword",
            Profile = new ProfileEntity { FirstName = "New", LastName = "User" },
            Address = new AddressEntity { StreetAddress = "New Street", City = "New City", PostalCode = "New Postal" },
            Role = new RoleEntity { RoleName = "NewRole" }
        };

        // Act
        var createdUser = await userRepository.CreateAsync(newUser);

        // Assert
        Assert.NotNull(createdUser);
        Assert.NotEqual(default, createdUser.Id); // Antag att Id är en sträng. Justera beroende på din entitetsdefinition.
        var userInDb = await _context.Users
            .Include(u => u.Profile)
            .Include(u => u.Address)
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Email == newUser.Email);
        Assert.NotNull(userInDb);
        Assert.Equal("newuser@domain.com", userInDb.Email);
        Assert.Equal("NewUserPassword", userInDb.Password);
        // Kontrollera ytterligare fält och relaterade entiteter som behövs.
    }


    [Fact]
    public async Task DeleteAsync_RemovesUserAndReturnsTrue()
    {
        // Arrange
        var userRepository = new UserRepository(_context);
        var user = new UserEntity
        {
            Email = "userToDelete@domain.com",
            Password = "password",
            // Antag att relaterade entiteter definieras här vid behov
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        // Act
        var result = await userRepository.DeleteAsync(u => u.Email == "userToDelete@domain.com");

        // Assert
        Assert.True(result);
        var userInDb = await _context.Users.FirstOrDefaultAsync(u => u.Email == "userToDelete@domain.com");
        Assert.Null(userInDb);
    }

}
