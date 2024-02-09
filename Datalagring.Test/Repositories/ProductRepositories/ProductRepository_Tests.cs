
using DbTest.Contexts;
using DbTest.Entities;
using DbTest.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Datalagring.Test.Repositories.ProductRepositories;

public class ProductRepository_Tests
{
    private readonly ProductCatalogContext _context = new(new DbContextOptionsBuilder<ProductCatalogContext>()
    .UseInMemoryDatabase($"{Guid.NewGuid()}")
    .Options);


    [Fact]
    public async Task GetAllAsync_ReturnsAllProductsWithRelatedEntities()
    {
        // Arrange
        var productRepository = new ProductRepository(_context);

        // Skapa och lägg till produkter med relaterade entiteter
        var products = new List<Product>
    {
        new() {
            ProductName = "Product 1",
            Description = "Test1",
            Price = 121,
            Image = new Image { ImageName = "imagetest1", ImageUrl = "urltest1" },
            Brand = new Brand { Name = "name1", Description = "desc1", LogoUrl = "logo1", WebsiteUrl = "weburl1" },
            Category = new Category { CategoryName = "kategori1" },
            Color = new Color { ColorName = "colorname1", ColorCode = "code1" },
            Size = new Size { SizeName = "size1" }
        },
        new() {
            ProductName = "Product2",
            Description = "Test2",
            Price = 122,
            Image = new Image { ImageName = "imagetest2", ImageUrl = "urltest2" },
            Brand = new Brand { Name = "name2", Description = "desc2", LogoUrl = "logo2", WebsiteUrl = "weburl2" },
            Category = new Category { CategoryName = "kategori2" },
            Color = new Color { ColorName = "colorname2", ColorCode = "code2" },
            Size = new Size { SizeName = "size2" }
        }
    };

        foreach (var product in products)
        {
            _context.Products.Add(product);
        }
        await _context.SaveChangesAsync();

        // Act
        var result = await productRepository.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        var resultList = result.ToList();
        Assert.Equal(products.Count, resultList.Count);
        foreach (var product in resultList)
        {
            Assert.NotNull(product.Image);
            Assert.NotNull(product.Brand);
            Assert.NotNull(product.Category);
            Assert.NotNull(product.Color);
            Assert.NotNull(product.Size);
        }
    }

    [Fact]
    public async Task GetAsync_ReturnsProductWithSpecifiedConditionAndRelatedEntities()
    {
        // Arrange
        var productRepository = new ProductRepository(_context);

        // Lägg till produkter med relaterade entiteter
        var productToAdd = new Product
        {
            ProductName = "SpecificProduct",
            Description = "SpecificDescription",
            Price = 123,
            Image = new Image { ImageName = "specificImage", ImageUrl = "specificUrl" },
            Brand = new Brand { Name = "specificBrand", Description = "brandDesc", LogoUrl = "brandLogo", WebsiteUrl = "brandWeb" },
            Category = new Category { CategoryName = "specificCategory" },
            Color = new Color { ColorName = "specificColor", ColorCode = "colorCode" },
            Size = new Size { SizeName = "specificSize" }
        };

        _context.Products.Add(productToAdd);
        await _context.SaveChangesAsync();

        // Act
        var result = await productRepository.GetAsync(p => p.ProductName == "SpecificProduct");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("SpecificProduct", result.ProductName);
        Assert.NotNull(result.Image);
        Assert.Equal("specificImage", result.Image.ImageName);
        Assert.NotNull(result.Brand);
        Assert.Equal("specificBrand", result.Brand.Name);
        Assert.NotNull(result.Category);
        Assert.Equal("specificCategory", result.Category.CategoryName);
        Assert.NotNull(result.Color);
        Assert.Equal("specificColor", result.Color.ColorName);
        Assert.NotNull(result.Size);
        Assert.Equal("specificSize", result.Size.SizeName);
    }

    [Fact]
    public async Task CreateAsync_AddsNewProductToDatabase()
    {
        // Arrange
        var productRepository = new ProductRepository(_context);
        var newProduct = new Product
        {
            ProductName = "NewProduct",
            Description = "New Description",
            Price = 100,
        };

        // Act
        var createdProduct = await productRepository.CreateAsync(newProduct);

        // Assert
        Assert.NotNull(createdProduct);
        Assert.NotEqual(default, createdProduct.Id);
        var productInDb = await _context.Products.FindAsync(createdProduct.Id);
        Assert.NotNull(productInDb);
        Assert.Equal("NewProduct", productInDb.ProductName);
        Assert.Equal("New Description", productInDb.Description);
        Assert.Equal(100, productInDb.Price);
    }

    [Fact]
    public async Task DeleteAsync_RemovesProductAndReturnsTrue()
    {
        // Arrange
        var productRepository = new ProductRepository(_context);
        var productToRemove = new Product
        {
            ProductName = "ProductToDelete",
            Description = "Description of product to delete",
            Price = 50,
        };

        _context.Products.Add(productToRemove);
        await _context.SaveChangesAsync();

        // Act
        var result = await productRepository.DeleteAsync(p => p.ProductName == "ProductToDelete");

        // Assert
        Assert.True(result);
        var deletedProduct = await _context.Products.FirstOrDefaultAsync(p => p.ProductName == "ProductToDelete");
        Assert.Null(deletedProduct);
    }

}
