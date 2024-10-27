using Application.Interface;
using Application.Mapper;
using Application.Service;
using Domain.Interface;
using Infrastructure.Adapter;
using Infrastructure.Configuration;
using Infrastructure.Interface;
using Infrastructure.Interfaces;
using Infrastructure.Repository;
using Infrastructure.Repository.Scylla;
using Microsoft.AspNetCore.Http.Json;
using System.Text.Json.Serialization;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Logging.ClearProviders();
        builder.Logging.AddConsole();

        // register services
        RegistryInfrastructureServices(builder);
        RegistryApplicationServices(builder);
        RegistryIncomingServices(builder);
        RegistryOutgoingServices(builder);

        builder.Services.AddHttpClient();

        builder.Services.Configure<JsonOptions>(opt =>
        {
            opt.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseMiddleware<ExceptionHandler>();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }

    private static void RegistryInfrastructureServices(WebApplicationBuilder builder)
    {
        // Configuration contexts
        builder.Services.AddSingleton<IScyllaContext, ScyllaContext>();

        // Domain repositories
        builder.Services.AddSingleton<ICategoryRepository, CategoryRepository>();

        // ScyllaDB repositories
        builder.Services.AddSingleton<ICategoryScyllaRepository, CategoryScyllaRepository>();

        // Adapters
        builder.Services.AddSingleton<ICategoryAdapter, CategoryAdapter>();

    }

    private static void RegistryApplicationServices(WebApplicationBuilder builder)
    {
        // Mappers
        builder.Services.AddSingleton<ICategoryMapper, CategoryMapper>();

        // Services
        builder.Services.AddSingleton<ICategoryServices, CategoryServices>();
    }

    private static void RegistryIncomingServices(WebApplicationBuilder builder)
    {

    }

    private static void RegistryOutgoingServices(WebApplicationBuilder builder)
    {

    }
}
