using LensUp.Common.AzureTableStorage;
using LensUp.GalleryService.Application.Abstractions;
using LensUp.GalleryService.Domain.Repositories;
using LensUp.GalleryService.Infrastructure.Repositories;
using LensUp.GalleryService.Infrastructure.Services;
using LensUp.GalleryService.Infrastructure.TableConfigurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LensUp.GalleryService.Infrastructure;

public static class DependencyInjection
{
    private const string AzureStorageKey = "AzureStorage";

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var azureTablesConnectionString = configuration.GetConnectionString(AzureStorageKey)
           ?? throw new Exception($"Value for key '{AzureStorageKey}' is null or not found in the configuration.");
        services.AddAzureTables(azureTablesConnectionString);

        services.AddActiveGalleryReadOnlyRepository();
        services.AddScoped<IGalleryNotificationService, GalleryNotificationService>();

        return services;
    }

    private static IServiceCollection AddActiveGalleryReadOnlyRepository(this IServiceCollection services)
    {
        services.AddAzureTableRepository(new ActiveGalleryTableConfiguration());
        services.AddScoped<IActiveGalleryReadOnlyRepository, ActiveGalleryReadOnlyRepository>();

        return services;
    }
}