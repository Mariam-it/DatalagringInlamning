using Datalagring.Entities;

namespace Datalagring.ViewModels;

public class UserViewModel
{

    public string Email { get; set; } = null!;


    public string Password { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? StreetName { get; set; }

    public string? PostalCode { get; set; }

    public string? City { get; set; }

    public string? RoleName { get; set; }

    public static implicit operator UserEntity(UserViewModel model)
    {
        return new UserEntity
        {
            Email = model.Email,
            Password = model.Password,
            Profile = new ProfileEntity
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
            },
            Role = new RoleEntity
            {
                RoleName = model.RoleName!,
            },
            Address = new AddressEntity
            {
                StreetAddress = model.StreetName,
                PostalCode = model.PostalCode,
                City = model.City,
            }
        };
    }
}
