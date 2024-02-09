using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Datalagring.Entities;

public class UserEntity
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Required]
    [Column(TypeName = "varchar(100)")]

    public string Email { get; set; } = null!;

    [Required]
    [Column(TypeName = "varchar(200)")]

    public string Password { get; set; } = null!;

    [Required]
    [ForeignKey(nameof(RoleEntity))]
    public int RoleId { get; set; }

    [Required]
    [ForeignKey(nameof(AddressEntity))]
    public int AddressId { get; set; }

    [Required]
    [ForeignKey(nameof(ProfileEntity))]
    public int ProfileId { get; set; }

    public virtual RoleEntity Role { get; set; } = null!;

    public virtual ProfileEntity Profile { get; set; } = null!;

    public virtual AddressEntity Address { get; set; } = null!;
}
