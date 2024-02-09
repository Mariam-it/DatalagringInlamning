using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Datalagring.Entities;

public partial class Brand
{
    [Key]
    public int Id { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;

    [StringLength(500)]
    public string? Description { get; set; }

    [StringLength(200)]
    public string? WebsiteUrl { get; set; }

    [StringLength(256)]
    public string? LogoUrl { get; set; }

    [InverseProperty("Brand")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
