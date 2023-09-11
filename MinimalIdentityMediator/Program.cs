using System.Text.Json;
using FluentValidation;
using Mediator;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MinimalIdentityMediator;
using MinimalIdentityMediator.Example;
using MinimalIdentityMediator.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddValidatorsFromAssemblyContaining<Program>(ServiceLifetime.Singleton)

    .AddAuthentication()
    .AddBearerToken(IdentityConstants.BearerScheme)
    .Services

    .AddAuthorizationBuilder()
    .Services

    .AddMediator(options => options.ServiceLifetime = ServiceLifetime.Scoped)
    .AddSingleton(typeof(IPipelineBehavior<,>), typeof(RequestValidatorBehavior<,>))

    .AddDbContext<AppDbContext>(x => x.UseSqlite("DataSource=app.db"))

    .AddIdentityCore<AppUser>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddApiEndpoints()
    .Services

    .AddEndpointsApiExplorer()
    .AddSwaggerGen()

    .Configure<JsonSerializerOptions>(options =>
    {
        options.WriteIndented = true;
        options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapIdentityApi<AppUser>()
    .WithTags("Account");

app.UseHttpsRedirection();

app.Mediate<ExampleRequest>(x => x.MapGet, "/example/{name}");
app.Run();

class AppUser : IdentityUser { }

class AppDbContext : IdentityDbContext<AppUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }
}