using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductApp.Entities;

public class UserEntity
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [Column(TypeName = "varchar(100)")]

    public string Email { get; set; } = null!;

    [Required]
    [Column(TypeName = "varchar(200)")]

    public string Password { get; set; } = null!;

    [Required]
    [ForeignKey(nameof(RoleEntity))]
    public int RoleId { get; set; }

    public virtual RoleEntity Role { get; set; } = null!;

    public virtual ProfileEntity Profile { get; set; } = null!;

}






