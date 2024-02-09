using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Datalagring.Entities;

public partial class Category
{
    [Key]
    public int Id { get; set; }

    [StringLength(80)]
    public string CategoryName { get; set; } = null!;

    [InverseProperty("Category")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
