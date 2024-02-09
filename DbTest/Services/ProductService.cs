using Datalagring.Contexts;
using Datalagring.Entities;
using Datalagring.Repositories;
using Datalagring.ViewModels;

namespace Datalagring.Services;

public class ProductService(ProductRepository productRepository, ProductCatalogContext dataContext)
{
    private readonly ProductRepository _productRepository = productRepository;
    private readonly ProductCatalogContext _dataContext = dataContext;




    public async Task<bool> CreateProduct(ProductViewModel model)
    {

        Product productEntity = model;

        try
        {
            var createdProduct = await _productRepository.CreateAsync(productEntity);
            return createdProduct != null;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            // Consider logging the exception here
            return false;
        }
    }
    public async Task<Product> GetProductByName(string name)
    {
        var productEntity = await _productRepository.GetAsync(x => x.ProductName == name);
        return productEntity;
    }
    public async Task<Product> GetUserByArticleNumber(string productName)
    {
        var productEntity = await _productRepository.GetAsync(x => x.ProductName == productName);
        return productEntity;
    }
    public async Task<IEnumerable<Product>> GetProducts()
    {
        var products = await _productRepository.GetAllAsync();
        return products;
    }
    public async Task<Product?> UpdateProduct(ProductViewModel model, Product existingProduct)
    {
        try
        {
            // Hämta den befintliga användaren baserat på e-postadress
            existingProduct = await _productRepository.GetAsync(x => x.ProductName == model.ProductName);
            if (existingProduct == null)
            {
                Console.WriteLine("Användaren finns inte.");
                return null;
            }

            //Uppdatera den befintliga användarens egenskaper med de nya värdena från modellen
            existingProduct.ProductName = model.ProductName!;
            existingProduct.Description = model.Description;
            existingProduct.Price = model.Price;


            // Spara ändringarna i databasen
            await _dataContext.SaveChangesAsync();

            return existingProduct;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }
    public async Task<bool> DeleteProduct(string productName)
    {
        try
        {
            // Kontrollerar först om användaren existerar
            bool productExists = await _productRepository.ExistingAsync(x => x.ProductName == productName);
            if (!productExists)
            {
                Console.WriteLine("Användaren finns inte.");
                return false; // Inget behov av att försöka ta bort om användaren inte finns
            }

            // Användaren finns, fortsätter med borttagningen
            var result = await _productRepository.DeleteAsync(x => x.ProductName == productName);
            if (result)
            {
                Console.Clear();
                return true; // Användaren togs bort framgångsrikt
            }
            else
            {
                Console.WriteLine("Det gick inte att ta bort användaren.");
                return false; // Det gick inte att ta bort användaren av någon anledning
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message); // Logga undantaget för felsökning
            return false; // Returnerar false om ett undantag uppstår
        }
    }

}
