using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace ProductApp.Entities;

public class RoleEntity
{
    [Key]
    public int Id { get; set; }


    [Required]
    [Column(TypeName = "varchar(50)")]

    public string RoleName { get; set; } = null!;

    public virtual ICollection<UserEntity> Users { get; set; } = new HashSet<UserEntity>();

}



public class SizeEntity
{

    public int Id { get; set; }

    [Column(TypeName = "nvarchar(6)")]

    public string SizeName { get; set; } = null!;
}

[PrimaryKey(nameof(ProductId), nameof(CategoryId))]
public class ProductCategoryEntity
{
    [ForeignKey(nameof(ProductId))]
    public string ProductId { get; set; } = null!;
    public ProductEntity Product { get; set; } = null!;

    [ForeignKey(nameof(Category))]
    public int CategoryId { get; set; }
    public CategoryEntity Category { get; set; } = null!;
}

   [PrimaryKey(nameof(ProductId), nameof(ImageId))]
public class ProductImageEntity
{
    [ForeignKey(nameof(ImageId))]
    public string ImageId { get; set; } = null!;
    public ImageEntity Image { get; set; } = null!;

    [ForeignKey(nameof(ProductId))]
    public string ProductId { get; set;} = null!;
    public ProductEntity Product { get; set; } = null!;
}

public class ImageEntity
{
    public Guid Id { get; set; }

    public string ImageName { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
    public ICollection<ProductImageEntity> ProductImages { get; set; } = new HashSet<ProductImageEntity>();
 
}
public class ProductEntity
{
    [Key]
    public string ArticleNumber { get; set; } = null!;

    [Column(TypeName = "nvarchar(100)")]
    public string ProductName { get; set; } = null!;
    public string? Description { get; set; }
    [Column(TypeName = "money")]
    public decimal? Price { get; set; }

    public ICollection<ProductCategoryEntity> Categories { get; set; } = new HashSet<ProductCategoryEntity>();
    public ICollection<ProductImageEntity> Images { get; set; } = new HashSet<ProductImageEntity>();
}

public class CategoryEntity
{
    public int Id { get; set; }

    [Column(TypeName = "nvarchar(80)")]
    public string CategoryName { get; set; } = null!;

    public ICollection<ProductCategoryEntity> Products { get; set; } = new HashSet<ProductCategoryEntity>();
}

public class ColorEntity
{
    public int Id { get; set; }

    [Column(TypeName = "nvarchar(25)")]
    public string ColorName { get; set; } = null!;

    [Column(TypeName = "nvarchar(25)")]
    public string ColorCode { get; set; } = null!;
} 






