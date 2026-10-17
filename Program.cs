using MediatR;
using Microsoft.EntityFrameworkCore;
using Stable.Repositories;
using Stable.Middleware;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddExceptionHandler<Prevent>();
builder.Services.AddProblemDetails();



builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services.AddTransient(typeof(IPipelineBehavior<, >), typeof(Qualifier<, >));
// Add services to the container.
builder.Services.AddDbContext<Storage>(config => config.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")).LogTo(Console.Write, LogLevel.Information));

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


var app = builder.Build();

app.UseExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
