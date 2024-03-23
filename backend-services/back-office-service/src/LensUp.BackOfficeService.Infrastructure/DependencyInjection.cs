using LensUp.BackOfficeService.Application.Abstractions;
using LensUp.BackOfficeService.Domain.Repositories;
using LensUp.BackOfficeService.Infrastructure.BlobStorage;
using LensUp.BackOfficeService.Infrastructure.Generators;
using LensUp.BackOfficeService.Infrastructure.Repositories;
using LensUp.BackOfficeService.Infrastructure.TableConfigurations;
using LensUp.Common.AzureBlobStorage;
using LensUp.Common.AzureTableStorage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LensUp.BackOfficeService.Infrastructure;

public static class DependencyInjection
{
    private const string AzureStorageKey = "AzureStorage";
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var azureTablesConnectionString = configuration.GetConnectionString(AzureStorageKey) 
            ?? throw new Exception($"Value for key '{AzureStorageKey}' is null or not found in the configuration.");
        services.AddAzureTables(azureTablesConnectionString)
                .AddAzureBlobStorage(azureTablesConnectionString);

        services
            .AddUserRepository()
            .AddGalleryRepository()
            .AddActiveGalleryRepository();

        services
            .AddScoped<IQRGenerator, QRGenerator>()
            .AddScoped<IGalleryStorageService, GalleryStorageService>()
            .AddScoped<IEnterCodeGenerator, EnterCodeGenerator>();

        return services;
    }

    private static IServiceCollection AddUserRepository(this IServiceCollection services)
    {
        services.AddAzureTableRepository(new UserTableConfiguration());
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }

    private static IServiceCollection AddGalleryRepository(this IServiceCollection services)
    {
        services.AddAzureTableRepository(new GalleryTableConfiguration());
        services.AddScoped<IGalleryRepository, GalleryRepository>();

        return services;
    }

    private static IServiceCollection AddActiveGalleryRepository(this IServiceCollection services)
    {
        services.AddAzureTableRepository(new ActiveGalleryTableConfiguration());
        services.AddScoped<IActiveGalleryRepository, ActiveGalleryRepository>();

        return services;
    }
}
