using System.ComponentModel.DataAnnotations;

namespace Datalagring.Entities;

public class AddressEntity
{
    [Key] 
    public int Id { get; set; }


    [MaxLength(50)]
    public string? StreetAddress { get; set; }


    [MaxLength(100)]
    public string? City { get; set; }


    [MaxLength(10)]
    public string? PostalCode { get; set; }

    [MaxLength(100)]
    public string? Country { get; set; }


    public virtual ICollection<UserEntity> Users { get; set; } = new HashSet<UserEntity>();

}
