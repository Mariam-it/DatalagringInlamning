using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductApp.Entities;

public class ProfileEntity
{
    [Key]
    [ForeignKey(nameof(UserEntity))]
    public Guid UserId { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string FirstName { get; set; } = null!;

    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string LastName { get; set; } = null!;

    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string? StreetName { get; set; }

    [Column(TypeName = "nvarchar(6)")]
    public string? PostalCode { get; set; }

    [Column(TypeName = "nvarchar(50)")]
    public string? City { get; set; }

    public virtual UserEntity User { get; set; } = null!;
}






