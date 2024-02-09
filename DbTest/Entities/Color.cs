using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Datalagring.Entities;

public partial class Color
{
    [Key]
    public int Id { get; set; }

    [StringLength(25)]
    public string ColorName { get; set; } = null!;

    [StringLength(25)]
    public string ColorCode { get; set; } = null!;

    [InverseProperty("Color")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
