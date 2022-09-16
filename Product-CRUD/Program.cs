using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductCRUD;
using ProductCRUD.Model;
using ProductCRUD.UseCase;
using ProductCRUD.Utils;

namespace ProductCrud;

public class Program
{
    //private readonly AppSettings _configuration;

    //public Program(IOptions<AppSettings> appSettings)
    //{
    //    _configuration = appSettings.Value;
    //}
    public static void Main(string[] args)
    {
        //var host = CreateHostBuilder(args).Build();
        // When scope is needed
        //var serviceScope = host.Services.CreateScope();
        //var provider = serviceScope.ServiceProvider;

        //var productUseCase = provider.GetRequiredService<ProductRegistrationUseCase>();
        //productUseCase.Handle(new Product("1", "Nasi Goreng"));
        //productUseCase.Handle(new Product("2", "Es teh tawar"));

        // When scope is not needed
        //var productUseCase = host.Services.GetRequiredService<ProductRegistrationUseCase>();
        //productUseCase.Handle(new Product("1", "Nasi Goreng"));
        //productUseCase.Handle(new Product("2", "Es teh tawar"));

        var startup = new Startup();

        // How to read configuration from appsettings.json
        // Way 1. Read from singleton
        var appName = startup.Provider.GetRequiredService<IConfiguration>().GetSection("App").GetSection("AppName")
            .Value;
        Console.WriteLine($"Run: {appName}");

        // Way 2. IOptions (use constructor injection)
        //Console.WriteLine($"File Directory: {_configuration.FileDirectory}");

        // Way 3. Binding
        //var appSettings = new AppSettings();
        //startup.Provider.GetRequiredService<IConfiguration>().GetSection("App").Bind(appSettings);
        //Console.WriteLine($"Version: {appSettings.Version}");
        var version = startup.Provider.GetRequiredService<AppSettings>().Version;
        Console.WriteLine($"Version: {version}");
        var fileDir = startup.Provider.GetRequiredService<AppSettings>().FileDirectory;
        Console.WriteLine($"File Directory: {fileDir}");

        // var productUseCase = startup.Provider.GetRequiredService<ProductRegistrationResolver>()(RepoType.DB);
        // productUseCase.Handle(new Product("1", "Somai"));
        // productUseCase.Handle(new Product("2", "Es kelapa muda"));

        // Check the singleton dependency
        // productUseCase = startup.Provider.GetRequiredService<ProductRegistrationResolver>()(RepoType.ARRAY);
        // productUseCase.Handle(new Product("1", "Nasi goreng"));
        // productUseCase.Handle(new Product("2", "Es teh manis"));

        // var bulkProductRegistration = startup.Provider.GetRequiredService<BulkProductRegistrationUseCase>();
        // bulkProductRegistration.Handle(new List<Product>()
        // {
        //     new Product("14", "Juice semangka"),
        //     // Should be rollback, because length of id is too long
        //     // new Product("111-222-333-444", "Juice alpukat"),
        //     new Product("15", "Juice strawberry")
        // });
        var findProductUseCase = startup.Provider.GetRequiredService<FindProductResolver>()(RepoType.DB);
        var products = findProductUseCase.Handle(ProductFindType.All);
        foreach (var p in products)
        {
            Console.WriteLine(p.ToString());
        }

        var productResId = findProductUseCase.Handle(ProductFindType.ById, "6");
        foreach (var p in productResId)
        {
            Console.WriteLine(p.ToString());
        }
    }

    //public static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args).ConfigureServices((_, services) => new Startup().ConfigureServices(services));
}