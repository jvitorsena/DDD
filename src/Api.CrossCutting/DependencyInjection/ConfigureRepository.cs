using Api.Data.Context;
using Api.Data.Implementations;
using Api.Data.Repository;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.Services;
using Api.Domain.Interfaces.Services.User;
using Api.Domain.Repository;
using Api.Domain.Security;
using Api.Service.Services;
using AutoMapper;
using crosscutting.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace crosscutting.DependencyInjection;

public static class MyConfigServiceCollectionExtensions
{
    public static IServiceCollection AddConfig(
         this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<MyContext>(options => options.UseMySql(config.GetConnectionString("MySQL"), Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.31-mysql")));

        return services;
    }

    public static IServiceCollection AddMyDependencyGroup(
         this IServiceCollection services)
    {
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<ILoginService, LoginService>();
        services.AddScoped<IUserRepository, UserImplementation>();
        services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));

        #region [AutoMapper]
        var config = new AutoMapper.MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new DtoToModelProfile());
            cfg.AddProfile(new EntityToDtoProfile());
            cfg.AddProfile(new ModelToEntityProfile());
        });
        IMapper mapper = config.CreateMapper();
        services.AddSingleton(mapper);
        #endregion

        var signConfigurations = new SigningConfigurations();
        services.AddSingleton(signConfigurations);

        return services;
    }
}


