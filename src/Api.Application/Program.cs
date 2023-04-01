using Api.Data.Context;
using Api.Data.Implementations;
using Api.Data.Repository;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.Services;
using Api.Domain.Interfaces.Services.User;
using Api.Domain.Repository;
using Api.Domain.Security;
using Api.Service.Services;
using crosscutting.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

IConfiguration AppSettings = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", true)
    .AddEnvironmentVariables()
    .Build();

#region [Dependency Injection]

builder.Services.AddDbContext<MyContext>(
    options => options.UseMySql("server=localhost;user=root;password=root_pwd;database=CSharpDDD", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.31-mysql"))
);
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserImplementation>();
builder.Services.AddTransient<ILoginService, LoginService>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
var signConfigurations = new SigningConfigurations();
builder.Services.AddSingleton(signConfigurations);
var tokenConfigurations = new TokenConfigurations();
new ConfigureFromConfigurationOptions<TokenConfigurations>(AppSettings.GetSection("TokenConfigurations")).Configure(tokenConfigurations);
builder.Services.AddSingleton(tokenConfigurations);

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
