using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Datalagring.Entities;

public partial class Size
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string? SizeName { get; set; }

    [InverseProperty("Size")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
