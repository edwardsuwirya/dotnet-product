using System;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductCRUD.Repository;
using ProductCRUD.UseCase;

namespace ProductCRUD
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        private readonly IServiceProvider provider;
        public IServiceProvider Provider => provider;

        public Startup()
        {
            configuration = readConfiguration();
            provider = ConfigureServices();
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
                .Configure<AppSettings>(configuration.GetSection("App"))
                .AddSingleton<IConfiguration>(configuration)
                .AddSingleton<AppSettings>((provider) =>
                {
                    var appSettings = new AppSettings();
                    var config = provider.GetRequiredService<IConfiguration>();
                    config.Bind("App", appSettings);
                    return appSettings;
                })
                .AddSingleton<IProductRepository>(_ => new ProductArrayRepository())
                .AddTransient<ProductRegistrationUseCase>(provider => new ProductRegistrationUseCase(provider.GetRequiredService<IProductRepository>()))
                .AddTransient<FindProductUseCase>(provider => new FindProductUseCase(provider.GetRequiredService<IProductRepository>()));
            return services.BuildServiceProvider();
        }
    }
}

