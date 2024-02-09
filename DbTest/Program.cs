using Datalagring.Contexts;
using Datalagring.Repositories;
using Datalagring.Services;
using Datalagring.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

class Program
{
    static async Task Main(string[] args)
    {
        var builder = Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                services.AddDbContext<DataContext>(x => x.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\moust\OneDrive\Desktop\WINN23\Datalagring\Project\DbTest\Data\local_Dbtest.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=True").LogTo(Console.WriteLine, LogLevel.Warning));
                services.AddDbContext<ProductCatalogContext>(options => options.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\moust\OneDrive\Desktop\WINN23\Datalagring\Project\DbTest\Data\DatabaseFirst.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=True").LogTo(Console.WriteLine, LogLevel.Information));
                services.AddScoped<AddressRepository>();
                services.AddScoped<BrandRepository>();
                services.AddScoped<CategoryRepository>();
                services.AddScoped<ColorRepository>();
                services.AddScoped<ImageRepository>();
                services.AddScoped<ProductRepository>();
                services.AddScoped<ProfileRepository>();
                services.AddScoped<RoleRepository>();
                services.AddScoped<SizeRepository>();
                services.AddScoped<UserRepository>();
                services.AddScoped<ProductService>();
                services.AddScoped<UserService>();
            });

        var host = builder.Build();
        var userService = host.Services.GetRequiredService<UserService>();
        var productService = host.Services.GetRequiredService<ProductService>();

        while (true)
        {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(" ****** Välkommen till systemet ****** ");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(" => 1. Hantera användare");
                Console.WriteLine(" => 2. Hantera produkter");
                Console.WriteLine(" X 3. Avsluta");
                Console.ResetColor();
                Console.Write("Välj ett alternativ: ");

                var mainChoice = Console.ReadLine();
                switch (mainChoice)
                {
                    case "1":
                        await UserMenu(userService);
                        break;
                    case "2":
                        await ProductMenu(productService);
                        break;
                    case "3":
                        return;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("!!! Ogiltigt val, försök igen. !!!");
                        Console.ResetColor();
                    break;
                }
                Console.WriteLine("\nTryck på valfri tangent för att fortsätta...");
                Console.ReadKey();
        }
    }

    static async Task UserMenu(UserService userService)
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(" ***** Användarhantering *****");
                Console.ResetColor ();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("1. Registrera en ny användare");
                Console.WriteLine("2. Visa alla användare");
                Console.WriteLine("3. Ta bort en användare");
                Console.WriteLine("4. Uppdatera en användare");
                Console.WriteLine("5. Återgå till huvudmenyn");
                Console.ResetColor();
                Console.Write("Välj ett alternativ: ");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        await RegisterUser(userService);
                        break;
                    case "2":
                        await ShowAllUsers(userService);
                        break;
                    case "3":
                        await DeleteUser(userService);
                        break;
                    case "4":
                        await UpdateUser(userService);
                        break;
                    case "5":
                        return; // Återgå till huvudmenyn
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(" !!!! Ogiltigt val, försök igen. !!!! ");
                        Console.ResetColor();
                    break;
                }
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("\nTryck på valfri tangent för att fortsätta...");
                Console.ResetColor();
                Console.ReadKey();
            }
    }
    static async Task ProductMenu(ProductService productService)
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(" ***** Produkthantering *****");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("1. Skapa en ny produkt");
                Console.WriteLine("2. Visa alla produkter");
                Console.WriteLine("3. Ta bort en produkt");
                Console.WriteLine("4. Uppdatera en produkt");
                Console.WriteLine("5. Återgå till huvudmenyn");
                Console.ResetColor();
                Console.Write("Välj ett alternativ: ");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        await CreateProduct(productService);
                        break;
                    case "2":
                        await ShowAllProducts(productService);
                        break;
                    case "3":
                        await DeleteProduct(productService);
                        break;
                    case "4":
                        await UpdateProduct(productService);
                        break;
                    case "5":
                        return; // Återgå till huvudmenyn
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(" !!! Ogiltigt val, försök igen. !!!");
                        Console.ResetColor();
                    break;
                }
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("\nTryck på valfri tangent för att fortsätta...");
                Console.ResetColor();
                Console.ReadKey();
            }

        }

    static async Task CreateProduct(ProductService productService)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("Skapa en ny produkt:");
        Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.Write("Produkt namn: ");
        var productName = Console.ReadLine() ?? "";

        Console.Write("Produkt beskrivning: ");
        var description = Console.ReadLine() ?? "";

        Console.Write("Produkt pris: ");
        var price = decimal.Parse(Console.ReadLine() ?? "");

        Console.Write("Produkt kategori: ");
        var categoryName = Console.ReadLine() ?? "";

        Console.Write("Produkt brand: ");
        var brandName = Console.ReadLine() ?? "";

        Console.Write("Produkt brand beskrivning: ");
        var brandDescription = Console.ReadLine() ?? "";

        Console.Write("Produkt brand website url: ");
        var brandWebsiteUrl = Console.ReadLine() ?? "";

        Console.Write("Produkt brand logo url: ");
        var brandLogoUrl = Console.ReadLine() ?? "";

        Console.Write("Produkt bild namn: ");
        var imageName = Console.ReadLine() ?? "";

        Console.Write("Produkt bild url: ");
        var imageUrl = Console.ReadLine() ?? "";

        Console.Write("Produkt storlek: ");
        var sizeName = Console.ReadLine() ?? "";

        Console.Write("Produkt färg namn: ");
        var colorName = Console.ReadLine() ?? "";

        Console.Write("Produkt färg kod: ");
        var colorCode = Console.ReadLine() ?? "";
        Console.ResetColor();

        var productViewModel = new ProductViewModel
        {
            ProductName = productName,
            Description = description,
            Price = price,
            CategoryName = categoryName,
            BrandName = brandName,
            BrandDescription = brandDescription,
            BrandWebsiteUrl = brandWebsiteUrl,
            BrandLogoUrl = brandLogoUrl,
            ImageName = imageName,
            ImageUrl = imageUrl,
            SizeName = sizeName,
            ColorName = colorName,
            ColorCode = colorCode,

        };

        var result = await productService.CreateProduct(productViewModel);

        if (result)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" ---- Produkten har skapats framgångsrikt! ----");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(" !!!!!!! Det gick inte att skapa produkten. Kontrollera inmatningsdatan och försök igen. !!!!!!! ");
            Console.ResetColor();
        }
    }
    static async Task ShowAllProducts(ProductService productService)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Alla registrerade användare:");
        Console.ResetColor();


        var products = await productService.GetProducts();
        Console.Clear();
        Console.WriteLine();
        foreach (var product in products)
        {
            Console.WriteLine($"Product: {product.ProductName} {product.Description} {product.Price} SEK {product.Size?.SizeName} {product.Color?.ColorName} {product.Brand?.Name} {product.Category?.CategoryName}");
        }
    }
    static async Task DeleteProduct(ProductService productService)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("Ange produkt namn för den produkt du vill ta bort: ");
        Console.ResetColor();
        var productName = Console.ReadLine() ?? "";
        var result = await productService.DeleteProduct(productName);
        if (result)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" --- Produkten har tagits bort. --- ");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(" ¤¤¤¤ Det gick inte att ta bort produkten. Kontrollera att produkt namn är korrekt och försök igen. ¤¤¤¤ ");
            Console.ResetColor();
        }
    }
    static async Task UpdateProduct(ProductService productService)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("-- Ange produkt namn för den produkt du vill uppdatera: ");
        Console.ResetColor();
        var name = Console.ReadLine() ?? "";

        // Försök att hämta den befintliga användaren med angiven e-post
        var existingProduct = await productService.GetProductByName(name);
        if (existingProduct == null)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(" :( Produkten kunde inte hittas.");
            Console.ResetColor();
            return;
        }
        Console.Clear();
        // Samla in uppdaterad information från användaren
        //Console.Write("Ange nytt lösenord (lämna tomt för att inte ändra): ");
        //var productName = Console.ReadLine();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write(" -- Ange nytt förnamn: ");
        Console.ResetColor();
        var description = Console.ReadLine() ?? existingProduct.Description; // Anta att Profile är laddad

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write(" -- Produkt pris: ");
        Console.ResetColor();
        string? input = Console.ReadLine();
        if (!decimal.TryParse(input, out decimal price))
        {
            price = existingProduct.Price ?? 0m; // Antag att existingProduct.Price är av typen decimal?
        }

        // Skapa en ny UserViewModel med uppdaterad information
        var productViewModel = new ProductViewModel
        {
            ProductName = name,
            Description = description,
            Price = price,
        };

        // Anropa UserService för att uppdatera användaren
        var updatedProduct = await productService.UpdateProduct(productViewModel, existingProduct);
        if (updatedProduct != null)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" :) Produkten har uppdaterats framgångsrikt. :) ");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(" ## Det gick inte att uppdatera produkten. ## ");
            Console.ResetColor();
        }
    }
    static async Task RegisterUser(UserService userService)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.WriteLine(" ** Registrera en ny användare:");
        Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write("E-post: ");
        var email = Console.ReadLine() ?? "";

        Console.Write("Lösenord: ");
        var password = Console.ReadLine() ?? "";

        Console.Write("Förnamn: ");
        var firstName = Console.ReadLine() ?? "";

        Console.Write("Efternamn: ");
        var lastName = Console.ReadLine() ?? "";

        Console.Write("Gatunamn: ");
        var streetName = Console.ReadLine() ?? "";

        Console.Write("Postnummer: ");
        var postalCode = Console.ReadLine() ?? "";

        Console.Write("Stad: ");
        var city = Console.ReadLine() ?? "";

        Console.Write("Rollnamn (användarens roll): ");
        var roleName = Console.ReadLine() ?? "";
        Console.ResetColor();

        var userViewModel = new UserViewModel
        {
            Email = email,
            Password = password,
            FirstName = firstName,
            LastName = lastName,
            StreetName = streetName,
            PostalCode = postalCode,
            City = city,
            RoleName = roleName,
        };

        var result = await userService.CreateUser(userViewModel);

        if (result)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" -- Användaren har registrerats framgångsrikt! -- ");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(" ¤¤ Det gick inte att registrera användaren. Kontrollera inmatningsdatan och försök igen. ¤¤ ");
            Console.ResetColor();
        }
    }
    static async Task ShowAllUsers(UserService userService)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.WriteLine("Alla registrerade användare:");
        Console.ResetColor();


        var users = await userService.GetUsers();
        Console.Clear();
        Console.WriteLine();
        foreach (var user in users)
        {
            Console.WriteLine($"E-post: {user.Email}");
        }
    }
    static async Task DeleteUser(UserService userService)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.Write(" -- Ange ID för den användare du vill ta bort: ");
        Console.ResetColor();
        var userEmail = Console.ReadLine() ?? "";
        var result = await userService.DeleteUser(userEmail);
        if (result)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" !!! Användaren har tagits bort. !!!");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(" ---- Det gick inte att ta bort användaren. Kontrollera att ID är korrekt och försök igen. ---- ");
            Console.ResetColor();
        }
    }
    static async Task UpdateUser(UserService userService)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.Write("Ange E-post för den användare du vill uppdatera: ");
        Console.ResetColor();
        var email = Console.ReadLine() ?? "";

        // Försök att hämta den befintliga användaren med angiven e-post
        var existingUser = await userService.GetUserByEmail(email);
        if (existingUser == null)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Användaren kunde inte hittas.");
            Console.ResetColor();
            return;
        }
        Console.Clear() ;
        // Samla in uppdaterad information från användaren
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.Write("Ange nytt lösenord (lämna tomt för att inte ändra): ");
        var password = Console.ReadLine();

        Console.Write("Ange nytt förnamn: ");
        var firstName = Console.ReadLine() ?? existingUser.Profile.FirstName; // Anta att Profile är laddad

        Console.Write("Ange nytt efternamn: ");
        var lastName = Console.ReadLine() ?? existingUser.Profile.LastName;

        // Antag att du vill uppdatera adressen också
        Console.Write("Ange ny gatunamn: ");
        var streetName = Console.ReadLine() ?? existingUser.Address.StreetAddress;

        Console.Write("Ange nytt postnummer: ");
        var postalCode = Console.ReadLine() ?? existingUser.Address.PostalCode;

        Console.Write("Ange ny stad: ");
        var city = Console.ReadLine() ?? existingUser.Address.City;
        Console.ResetColor() ;

        // Skapa en ny UserViewModel med uppdaterad information
        var userViewModel = new UserViewModel
        {
            Email = email,
            Password = password ?? existingUser.Password,
            FirstName = firstName ?? existingUser.Profile.FirstName,
            LastName = lastName ?? existingUser.Profile.LastName,
            StreetName = streetName ?? existingUser.Address.StreetAddress,
            PostalCode = postalCode ?? existingUser.Address.PostalCode,
            City = city ?? existingUser.Address.City,
            RoleName = existingUser.Role.RoleName
        };

        // Anropa UserService för att uppdatera användaren
        var updatedUser = await userService.UpdateUser(userViewModel, existingUser);
        if (updatedUser != null)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Användaren har uppdaterats framgångsrikt.");
            Console.WriteLine();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Det gick inte att uppdatera användaren.");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
        }
    }
}