using Microsoft.EntityFrameworkCore;
using Stable.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(Program).Assembly));
// Add services to the container.
builder.Services.AddDbContext<Storage>(config => config.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")).LogTo(Console.Write, LogLevel.Information));

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
