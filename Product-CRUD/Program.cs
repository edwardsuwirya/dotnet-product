using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProductCRUD.Model;
using ProductCRUD.Repository;
using ProductCRUD.UseCase;
using ProductCRUD.Utils;
using ProductCRUD;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

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
        var appName = startup.Provider.GetRequiredService<IConfiguration>().GetSection("App").GetSection("AppName").Value;
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

        var productUseCase = startup.Provider.GetRequiredService<ProductRegistrationUseCase>();
        productUseCase.Handle(new Product("1", "Nasi Goreng"));
        productUseCase.Handle(new Product("2", "Es teh tawar"));

        var findProductUseCase = startup.Provider.GetRequiredService<FindProductUseCase>();
        var products = findProductUseCase.Handle(ProductFindType.All);
        foreach (var p in products)
        {
            Console.WriteLine(p.ToString());
        }

        var productResId = findProductUseCase.Handle(ProductFindType.ById, "2");
        foreach (var p in productResId)
        {
            Console.WriteLine(p.ToString());
        }

    }

    //public static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args).ConfigureServices((_, services) => new Startup().ConfigureServices(services));

}
