using DbTest.Contexts;
using DbTest.Entities;
using DbTest.Repositories;
using DbTest.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Datalagring.Test.Services.ProductService;


public class ProductService_Tests
{
    private readonly ProductCatalogContext _context = new(new DbContextOptionsBuilder<ProductCatalogContext>()
    .UseInMemoryDatabase($"{Guid.NewGuid()}")
    .Options);

    [Fact]
    public async Task CreateProduct_ReturnsTrue_WhenProductIsCreated()
    {
        // Arrange
        var productRepository = new ProductRepository(_context);
        var productService = new DbTest.Services.ProductService(productRepository, _context);

        var productViewModel = new ProductViewModel
        {
            ProductName = "Test Product",
            Description = "Description",
            Price = 9.99m,
            CategoryName = "category",
            BrandName = "brand",
            BrandDescription = "desc",
            BrandWebsiteUrl = "weburl",
            BrandLogoUrl = "logo",
            ImageName = "image",
            ImageUrl = "imgurl",
            SizeName = "size",
            ColorName = "color",
            ColorCode = "code",
        };

        // Act
        var result = await productService.CreateProduct(productViewModel);

        // Assert
        Assert.True(result);
        var productInDb = await _context.Products.FirstOrDefaultAsync(p => p.ProductName == productViewModel.ProductName);
        Assert.NotNull(productInDb);
        Assert.Equal(productViewModel.Price, productInDb.Price);
    }

    [Fact]
    public async Task GetProductByName_ReturnsProduct_WhenNameExists()
    {
        // Arrange
        var productRepository = new ProductRepository(_context);
        var productService = new DbTest.Services.ProductService(productRepository, _context);
        var testProductName = "Test Product";
        var product = new Product
        {
            ProductName = testProductName,
            Description = "Test Description",
            Price = 9.99m,
            Image = new Image { ImageName = "imagetest1", ImageUrl = "urltest1" },
            Brand = new Brand { Name = "name1", Description = "desc1", LogoUrl = "logo1", WebsiteUrl = "weburl1" },
            Category = new Category { CategoryName = "kategori1" },
            Color = new Color { ColorName = "colorname1", ColorCode = "code1" },
            Size = new Size { SizeName = "size1" }
        };

        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();

        // Act
        var resultProduct = await productService.GetProductByName(testProductName);

        // Assert
        Assert.NotNull(resultProduct);
        Assert.Equal(testProductName, resultProduct.ProductName);
    }

    [Fact]
    public async Task GetProductByName_ReturnsNull_WhenNameDoesNotExist()
    {
        // Arrange
        var productRepository = new ProductRepository(_context);
        var productService = new DbTest.Services.ProductService(productRepository, _context);
        var nonExistentProductName = "Nonexistent Product";

        // Act
        var resultProduct = await productService.GetProductByName(nonExistentProductName);

        // Assert
        Assert.Null(resultProduct);
    }

    [Fact]
    public async Task GetProducts_ReturnsAllProducts()
    {
        // Arrange
        var productRepository = new ProductRepository(_context);
        var productService = new DbTest.Services.ProductService(productRepository, _context);

        // Lägg till testprodukter i databasen
        var addProducts = new List<Product>
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
            ProductName = "Product 2",
            Description = "Test2",
            Price = 122,
            Image = new Image { ImageName = "imagetest2", ImageUrl = "urltest2" },
            Brand = new Brand { Name = "name2", Description = "desc2", LogoUrl = "logo2", WebsiteUrl = "weburl2" },
            Category = new Category { CategoryName = "kategori2" },
            Color = new Color { ColorName = "colorname2", ColorCode = "code2" },
            Size = new Size { SizeName = "size2" }
        }
    };

        foreach (var product in addProducts)
        {
            _context.Products.Add(product);
        }
        await _context.SaveChangesAsync();

        // Act
        var products = await productService.GetProducts();

        // Assert
        Assert.NotNull(products);
        var productList = products.ToList();
        Assert.Equal(2, productList.Count); // Antag att det bara finns två produkter i databasen för detta test
        Assert.Contains(productList, p => p.ProductName == "Product 1");
        Assert.Contains(productList, p => p.ProductName == "Product 2");
    }

    [Fact]
    public async Task UpdateProduct_UpdatesExistingProduct()
    {
        // Arrange
        var productRepository = new ProductRepository(_context);
        var productService = new DbTest.Services.ProductService(productRepository, _context);
        var existingProduct = new Product { ProductName = "Existing Product", Description = "Old Description", Price = 100,
            Image = new Image { ImageName = "imagetest1", ImageUrl = "urltest1" },
            Brand = new Brand { Name = "name1", Description = "desc1", LogoUrl = "logo1", WebsiteUrl = "weburl1" },
            Category = new Category { CategoryName = "kategori1" },
            Color = new Color { ColorName = "colorname1", ColorCode = "code1" },
            Size = new Size { SizeName = "size1" }
        };
        await _context.Products.AddAsync(existingProduct);
        await _context.SaveChangesAsync();

        var updatedModel = new ProductViewModel { ProductName = "Existing Product", Description = "New Description", Price = 150 };

        // Act
        var updatedProduct = await productService.UpdateProduct(updatedModel, existingProduct);

        // Assert
        Assert.NotNull(updatedProduct);
        Assert.Equal("New Description", updatedProduct.Description);
        Assert.Equal(150, updatedProduct.Price);
    }

}
