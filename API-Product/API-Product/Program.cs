
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

        // Register services
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

        // Configure ExceptionHandler
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
        builder.Services.AddSingleton<IProductRepository, ProductRepository>();

        // ScyllaDB repositories
        builder.Services.AddSingleton<IProductScyllaRepository, ProductScyllaRepository>();
        builder.Services.AddSingleton<ICategoryScyllaRepository, CategoryScyllaRepository>();
        builder.Services.AddSingleton<ISellerScyllaRepository, SellerScyllaRepository>();
        builder.Services.AddSingleton<IStockScyllaRepository, StockScyllaRepository>();

        // Adapters
        builder.Services.AddSingleton<IProductAdapter, ProductAdapter>();
        builder.Services.AddSingleton<ISellerAdapter, SellerAdapter>();
    }

    private static void RegistryApplicationServices(WebApplicationBuilder builder)
    {
        // Mappers
        builder.Services.AddSingleton<IProductMapper, ProductMapper>();

        // Services
        builder.Services.AddSingleton<IProductServices, ProductServices>();
    }

    private static void RegistryIncomingServices(WebApplicationBuilder builder)
    {
        
    }

    private static void RegistryOutgoingServices(WebApplicationBuilder builder)
    {

    }
}


