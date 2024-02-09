using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Datalagring.Entities;

public class ProfileEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string FirstName { get; set; } = null!;

    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string LastName { get; set; } = null!;

    public virtual ICollection<UserEntity> Users { get; set; } = new HashSet<UserEntity>();
}
