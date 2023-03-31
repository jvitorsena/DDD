using Api.Data.Context;
using Api.Data.Repository;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.Services;
using Api.Service.Services;
using crosscutting.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<MyContext>(
    options => options.UseMySql("server=localhost;user=root;password=root_pwd;database=CSharpDDD", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.31-mysql"))
);
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));

// builder.Services.Add(ConfigureService.ConfigureDependenciesService(builder.Services));
// builder.Services.Add(ConfigureRepository.ConfigureDependenciesRepository(builder.Services));
// builder.Services.add

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // app.UseSwagger();
    // app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
