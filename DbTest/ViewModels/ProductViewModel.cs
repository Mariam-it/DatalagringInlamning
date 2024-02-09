using Datalagring.Entities;

namespace Datalagring.ViewModels;

public class ProductViewModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? ProductName { get; set; } 
    public string? Description { get; set; }
    public decimal? Price { get; set; }

    public string? CategoryName { get; set; }
    public string? BrandName { get; set; }
    public string? BrandDescription { get; set; }
    public string? BrandWebsiteUrl { get; set; }
    public string? BrandLogoUrl { get; set; }

    public Guid ImageId { get; set; } = Guid.NewGuid();
    public string? ImageName { get; set; } 
    public string? ImageUrl { get; set; }
    public string? SizeName { get; set; }
    public string? ColorName { get; set; }
    public string? ColorCode { get; set; }

    public static implicit operator Product(ProductViewModel model)
    {
        return new Product
        {
            Id = model.Id,
            ProductName = model.ProductName!,
            Description = model.Description,
            Price = model.Price,
            Category = new Category { CategoryName = model.CategoryName! },
            Brand = new Brand
            {
                Name = model.BrandName!,
                Description = model.Description,
                WebsiteUrl = model.BrandWebsiteUrl,
                LogoUrl = model.BrandLogoUrl,
            },
            Image = new Image { Id = model.ImageId ,ImageName = model.ImageName!, ImageUrl = model.ImageUrl! },
            Size = new Size { SizeName = model.SizeName! },
            Color = new Color { ColorName = model.ColorName!, ColorCode = model.ColorCode! }
        };

        
    }
}
