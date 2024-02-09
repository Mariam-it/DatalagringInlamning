using Datalagring.Contexts;
using Datalagring.Entities;
using Datalagring.Repositories;
using Datalagring.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Datalagring.Services;

public class UserService(DataContext dataContext, UserRepository userRepository)
{
    private readonly UserRepository _userRepository = userRepository;
    private readonly DataContext _dataContext = dataContext;

    public async Task<bool> CreateUser(UserViewModel model)
    {

        UserEntity userEntity = model;

        try
        {
            var createdUser = await _userRepository.CreateAsync(userEntity);
            return createdUser != null;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }
    public async Task<UserEntity> GetUserByEmail(string email)
    {
        var userEntity = await _userRepository.GetAsync(x => x.Email == email);
        return userEntity;
    }
    public async Task<UserEntity> GetUserById(string id)
    {
        var userEntity = await _userRepository.GetAsync(x => x.Id == id);
        return userEntity;
    }
    public async Task<IEnumerable<UserEntity>> GetUsers()
    {
        var users = await _userRepository.GetAllAsync();
        return users;
    }
    public async Task<UserEntity?> UpdateUser(UserViewModel model, UserEntity existingUser)
    {
        try
        {
            // Hämta den befintliga användaren baserat på e-postadress
            existingUser = await _userRepository.GetAsync(x => x.Email == model.Email);
            if (existingUser == null)
            {
                Console.WriteLine("Användaren finns inte.");
                return null;
            }

            //Uppdatera den befintliga användarens egenskaper med de nya värdena från modellen
            existingUser.Password = model.Password;
            existingUser.Profile.FirstName = model.FirstName;
            existingUser.Profile.LastName = model.LastName;

            // Uppdatera adressen
            existingUser.Address.StreetAddress = model.StreetName;
            existingUser.Address.City = model.City;
            existingUser.Address.PostalCode = model.PostalCode;

            // Spara ändringarna i databasen
            await _dataContext.SaveChangesAsync();

            return existingUser;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }
    public async Task<bool> DeleteUserById(string id)
    {
        var userEntity = await _userRepository.GetAsync(x => x.Id == id);
        if (userEntity != null)
            return true;
        return false;
    }
    public async Task<bool> DeleteUser(string email)
    {
        try
        {
            // Kontrollerar först om användaren existerar
            bool userExists = await _userRepository.ExistingAsync(x => x.Email == email);
            if (!userExists)
            {
                Console.WriteLine("Användaren finns inte.");
                return false; // Inget behov av att försöka ta bort om användaren inte finns
            }

            // Användaren finns, fortsätter med borttagningen
            var result = await _userRepository.DeleteAsync(x => x.Email == email);
            if (result)
            {
                Console.Clear();
                return true; // Användaren togs bort framgångsrikt
            }
            else
            {
                Console.WriteLine("Det gick inte att ta bort användaren.");
                return false; // Det gick inte att ta bort användaren av någon anledning
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message); // Logga undantaget för felsökning
            return false; // Returnerar false om ett undantag uppstår
        }
    }

}



