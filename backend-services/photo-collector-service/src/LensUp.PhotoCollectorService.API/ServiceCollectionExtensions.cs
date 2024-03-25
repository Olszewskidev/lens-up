using LensUp.PhotoCollectorService.API.Channels;
using LensUp.PhotoCollectorService.API.Services;
using LensUp.PhotoCollectorService.API.Validators;
using LensUp.Common.AzureQueueStorage;
using LensUp.Common.AzureBlobStorage;
using LensUp.Common.AzureTableStorage;

namespace LensUp.PhotoCollectorService.API;

internal static class ServiceCollectionExtensions
{
    private const string AzureStorageKey = "AzureStorage";

    public static IServiceCollection AddChannels(this IServiceCollection services)
    {
        services
            .AddSingleton<IPhotoChannel, PhotoChannel>();

        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services
            .AddSingleton<IPhotoProcessor, PhotoProcessor>()
            .AddHostedService<BackgroundPhotoProcessor>();

        return services;
    }

    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services
            .AddScoped<IUploadPhotoToGalleryRequestValidator, UploadPhotoToGalleryRequestValidator>();

        return services;
    }

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var azureTablesConnectionString = configuration.GetConnectionString(AzureStorageKey)
            ?? throw new Exception($"Value for key '{AzureStorageKey}' is null or not found in the configuration.");

        services
            .AddAzureQueue(azureTablesConnectionString)
            .AddAzureTables(azureTablesConnectionString)
            .AddAzureBlobStorage(azureTablesConnectionString);

        services
            .AddScoped<IPhotoQueueSender, PhotoQueueSender>();

        return services;
    }
}
