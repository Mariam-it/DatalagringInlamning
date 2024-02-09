using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Datalagring.Entities;

public partial class Product
{
    [Key]
    public Guid Id { get; set; }

    [StringLength(100)]
    public string ProductName { get; set; } = null!;

    public string? Description { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal? Price { get; set; }

    public int CategoryId { get; set; }

    public int ColorId { get; set; }

    public Guid? ImageId { get; set; }

    public int SizeId { get; set; }

    public int BrandId { get; set; }

    [ForeignKey("BrandId")]
    [InverseProperty("Products")]
    public virtual Brand Brand { get; set; } = null!;

    [ForeignKey("CategoryId")]
    [InverseProperty("Products")]
    public virtual Category Category { get; set; } = null!;

    [ForeignKey("ColorId")]
    [InverseProperty("Products")]
    public virtual Color Color { get; set; } = null!;

    [ForeignKey("ImageId")]
    [InverseProperty("Products")]
    public virtual Image? Image { get; set; }

    [ForeignKey("SizeId")]
    [InverseProperty("Products")]
    public virtual Size Size { get; set; } = null!;
}
