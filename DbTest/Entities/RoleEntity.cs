using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Datalagring.Entities;

public class RoleEntity
{
    [Key]
    public int Id { get; set; }


    [Required]
    [Column(TypeName = "varchar(50)")]

    public string RoleName { get; set; } = null!;

    public virtual ICollection<UserEntity> Users { get; set; } = new HashSet<UserEntity>();
}
