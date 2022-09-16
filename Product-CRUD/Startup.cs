using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Product_CRUD;
using ProductCRUD.Repository;
using ProductCRUD.UseCase;
using ProductCRUD.Utils;

namespace ProductCRUD
{
    public delegate ProductRegistrationUseCase ProductRegistrationResolver(RepoType key);

    public delegate FindProductUseCase FindProductResolver(RepoType key);

    public class Startup
    {
        private readonly IConfiguration _configuration;

        public IServiceProvider Provider { get; }

        public Startup()
        {
            _configuration = readConfiguration();
            Provider = ConfigureServices();
        }

        //public void ConfigureServices(IServiceCollection services)
        //{
        //    services
        //   .AddSingleton<IProductRepository>(_ => new ProductArrayRepository())
        //   .AddTransient<ProductRegistrationUseCase>(provider => new ProductRegistrationUseCase(provider.GetRequiredService<IProductRepository>()))
        //   .AddTransient<FindProductUseCase>(provider => new FindProductUseCase(provider.GetRequiredService<IProductRepository>()));
        //}

        private IConfiguration readConfiguration()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true)
                .AddEnvironmentVariables().Build();
        }

        private IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();
            services
                .Configure<AppSettings>(_configuration.GetSection("App")) // For using IOptions
                .AddSingleton<IConfiguration>(_configuration)
                .AddSingleton<DapperContext>(provider =>
                {
                    var config = provider.GetRequiredService<IConfiguration>();
                    var connString = config.GetConnectionString("DefaultConnection");
                    return new DapperContext(connString);
                })
                .AddSingleton<AppSettings>((provider) => // For using Binding
                {
                    var appSettings = new AppSettings();
                    var config = provider.GetRequiredService<IConfiguration>();
                    config.Bind("App", appSettings);
                    return appSettings;
                })
                .AddSingleton<IProductArrayRepository>(_ => new ProductArrayRepository())
                .AddTransient<IProductFileRepository>((provider) =>
                {
                    var appSettings = provider.GetRequiredService<AppSettings>();
                    var filePath = appSettings.FileDirectory + appSettings.FileName;
                    return new ProductFileRepository(filePath);
                })
                .AddTransient<IProductDbRepository, ProductDbRepository>()
                .AddTransient<ProductRegistrationResolver>(provider => key =>
                    {
                        switch (key)
                        {
                            case RepoType.ARRAY:
                                return new ProductRegistrationUseCase(
                                    provider.GetRequiredService<IProductArrayRepository>());
                            case RepoType.FILE:
                                return new ProductRegistrationUseCase(
                                    provider.GetRequiredService<IProductFileRepository>());
                            case RepoType.DB:
                                return new ProductRegistrationUseCase(
                                    provider.GetRequiredService<IProductDbRepository>());
                            default: throw new Exception("Key not found");
                        }
                    }
                )
                .AddTransient<FindProductResolver>(provider => key =>
                    {
                        switch (key)
                        {
                            case RepoType.ARRAY:
                                return new FindProductUseCase(provider.GetRequiredService<IProductArrayRepository>());
                            case RepoType.FILE:
                                return new FindProductUseCase(provider.GetRequiredService<IProductFileRepository>());
                            case RepoType.DB:
                                return new FindProductUseCase(provider.GetRequiredService<IProductDbRepository>());
                            default: throw new Exception("Key not found");
                        }
                    }
                ).AddTransient<BulkProductRegistrationUseCase>(provider =>
                    new BulkProductRegistrationUseCase(provider.GetRequiredService<IProductDbRepository>()));
            return services.BuildServiceProvider();
        }
    }
}