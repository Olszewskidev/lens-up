using LensUp.PhotoCollectorService.API.Channels;
using LensUp.PhotoCollectorService.API.Services;
using LensUp.PhotoCollectorService.API.Validators;
using LensUp.Common.AzureQueueStorage;
using LensUp.Common.AzureBlobStorage;
using LensUp.Common.AzureTableStorage;
using LensUp.PhotoCollectorService.API.DataAccess.ActiveGallery;
using LensUp.PhotoCollectorService.API.DataAccess.GalleryPhoto;
using LensUp.Common.Types;

namespace LensUp.PhotoCollectorService.API;

internal static class ServiceCollectionExtensions
{
    private const string AzureStorageKey = "AzureStorage";

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var azureStorageConnectionString = configuration.GetConnectionString(AzureStorageKey)
            ?? throw new Exception($"Value for key '{AzureStorageKey}' is null or not found in the configuration.");

        services
            .AddAzureTables(azureStorageConnectionString)
            .AddAzureQueue(azureStorageConnectionString)
            .AddAzureBlobStorage(azureStorageConnectionString);

        services
            .AddChannels()
            .AddServices()
            .AddValidators()
            .AddDataAccess()
            .AddIdGenerator();

        return services;
    }

    private static IServiceCollection AddChannels(this IServiceCollection services)
    {
        services
            .AddSingleton<IPhotoChannel, PhotoChannel>();

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services
            .AddSingleton<IPhotoQueueSender, PhotoQueueSender>()
            .AddScoped<IPhotoProcessor, PhotoProcessor>()
            .AddHostedService<BackgroundPhotoProcessor>();

        return services;
    }

    private static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services
            .AddScoped<IUploadPhotoToGalleryRequestValidator, UploadPhotoToGalleryRequestValidator>();

        return services;
    }

    private static IServiceCollection AddDataAccess(this IServiceCollection services)
    {
        services.AddAzureTableRepository(new ActiveGalleryTableConfiguration());
        services.AddScoped<IActiveGalleryRepository, ActiveGalleryRepository>();

        services.AddAzureTableRepository(new GalleryPhotoTableConfiguration());
        services.AddScoped<IGalleryPhotoRepository, GalleryPhotoRepository>();

        return services;
    }
}
