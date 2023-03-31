using Api.Domain.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace crosscutting.DependencyInjection;

public class ConfigureService
{
    public static IServiceCollection ConfigureDependenciesService(IServiceCollection serviceCollection)
    {
        return serviceCollection.AddTransient<IUserService, IUserService>();
    }
}