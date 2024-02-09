using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Datalagring.Entities;

public partial class Image
{
    [Key]
    public Guid Id { get; set; }

    [StringLength(256)]
    public string ImageName { get; set; } = null!;

    public string ImageUrl { get; set; } = null!;

    [InverseProperty("Image")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
