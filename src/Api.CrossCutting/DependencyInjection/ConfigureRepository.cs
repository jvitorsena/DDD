using Api.Data.Context;
using Api.Data.Implementations;
using Api.Data.Repository;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.Services;
using Api.Domain.Interfaces.Services.User;
using Api.Domain.Repository;
using Api.Service.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace crosscutting.DependencyInjection;

public static class MyConfigServiceCollectionExtensions
{
    public static IServiceCollection AddConfig(
         this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<MyContext>(options => options.UseMySql("server=localhost;user=root;password=root_pwd;database=CSharpDDD", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.31-mysql")));
        return services;
    }

    public static IServiceCollection AddMyDependencyGroup(
         this IServiceCollection services)
    {
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<ILoginService, LoginService>();
        services.AddScoped<IUserRepository, UserImplementation>();
        services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));

        return services;
    }
}


