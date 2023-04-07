using System.Data;
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
using crosscutting.DependencyInjection;
using crosscutting.Mappings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Curso de API com AspNetCore 7.0",
        Description = "Arquitetura DDD",
        TermsOfService = new Uri("https://github.com/jvitorsena/DDD"),
        Contact = new OpenApiContact
        {
            Name = "João Vitor",
            Email = "jvitorsena96@gmail.com",
            Url = new Uri("https://github.com/jvitorsena")
        },
        License = new OpenApiLicense
        {
            Name = "Termo de licença de uso",
            Url = new Uri("https://github.com/jvitorsena/DDD")
        }
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Entre com o token JWT",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            }, new List<string>()
        }
    });
});

IConfiguration AppSettings = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", true)
    .AddEnvironmentVariables()
    .Build();

#region [Dependency Injection]

var config = new AutoMapper.MapperConfiguration(cfg =>
{
    cfg.AddProfile(new DtoToModelProfile());
    cfg.AddProfile(new EntityToDtoProfile());
    cfg.AddProfile(new ModelToEntityProfile());
});
IMapper mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);
// builder.Services.AddDbContext<MyContext>(
//     options => options.UseMySql("server=localhost;user=root;password=root_pwd;database=CSharpDDD", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.31-mysql"))
// );
// builder.Services.AddTransient<IUserService, UserService>();
// builder.Services.AddScoped<IUserRepository, UserImplementation>();
// builder.Services.AddTransient<ILoginService, LoginService>();
// builder.Services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
builder.Services.AddConfig(builder.Configuration).AddMyDependencyGroup();
var signConfigurations = new SigningConfigurations();
builder.Services.AddSingleton(signConfigurations);
var tokenConfigurations = new TokenConfigurations();
new ConfigureFromConfigurationOptions<TokenConfigurations>(AppSettings.GetSection("TokenConfigurations")).Configure(tokenConfigurations);
builder.Services.AddSingleton(tokenConfigurations);
#endregion

#region [JWT Configuration]

builder.Services.AddAuthentication(authOption =>
{
    authOption.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    authOption.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    var paramsValidation = options.TokenValidationParameters;
    paramsValidation.IssuerSigningKey = signConfigurations.Key;
    paramsValidation.ValidAudience = tokenConfigurations.Audience;
    paramsValidation.ValidIssuer = tokenConfigurations.Issuer;
    paramsValidation.ValidateIssuerSigningKey = true;
    paramsValidation.ValidateLifetime = true;
    paramsValidation.ClockSkew = TimeSpan.Zero;
});
builder.Services.AddAuthorization(auth =>
{
    auth.AddPolicy("Bearer" , new AuthorizationPolicyBuilder()
        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
        .RequireAuthenticatedUser().Build());
});

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
