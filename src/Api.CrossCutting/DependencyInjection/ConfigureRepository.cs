using Api.Data.Context;
using Api.Data.Repository;
using Api.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace crosscutting.DependencyInjection;

public class ConfigureRepository
{
    public static IServiceCollection ConfigureDependenciesRepository(IServiceCollection serviceCollection)
    {
        return serviceCollection.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
        // serviceCollection.AddDbContext<MyContext>(
        //     options => options.UseMySql("server=localhost;user=root;password=root_pwd;database=CSharpDDD", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.31-mysql"))
        // );
    }
}