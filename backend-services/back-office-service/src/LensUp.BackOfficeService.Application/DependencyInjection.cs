using Microsoft.Extensions.DependencyInjection;

namespace LensUp.BackOfficeService.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            return services;
        }
    }
}
