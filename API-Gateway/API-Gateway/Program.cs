using Application.Client;
using Adapter.Interfaces;
using Application.Interfaces;
using Application.Mappings;
using Application.Services;
using Domain.Interfaces;
using Infrastructure.Interfaces;
using Infrastructure.Repository.Scylla;
using Infrastructure.Configuration;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Http.Json;
using Outgoing.Http;
using Outgoing.Http.Refit;
using Refit;
using System.Text.Json.Serialization;
using Application.Interface;
using Application.Mapper;

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
        builder.Services.AddSingleton<ILogRepository, LogRepository>();

        // ScyllaDB repositories
        builder.Services.AddSingleton<ILogScyllaRepository, LogScyllaRepository>();

        // Adapters
        builder.Services.AddSingleton<ILogAdapter, LogAdapter>();

    }   

    private static void RegistryApplicationServices(WebApplicationBuilder builder)
    {
        // Mappers
        builder.Services.AddSingleton<ILogMapper, LogMapper>();
        builder.Services.AddSingleton<ICustomerMapper, CustomerMapper>();

        // Services
        builder.Services.AddSingleton<ILogServices, LogServices>();
    }

    private static void RegistryIncomingServices(WebApplicationBuilder builder)
    {
        
    }

    private static void RegistryOutgoingServices(WebApplicationBuilder builder)
    {
        // Refit
        // - Customer
        builder.Services.AddRefitClient<ICustomerRefitClient>()
            .ConfigureHttpClient((sp, c) =>
             {
                 var config = sp.GetRequiredService<IConfiguration>();
                 c.BaseAddress = new Uri(config["Customer:BaseUrl"]);
             });
        // - Seller
        builder.Services.AddRefitClient<ISellerRefitClient>()
            .ConfigureHttpClient((sp, c) =>
            {
                var config = sp.GetRequiredService<IConfiguration>();
                c.BaseAddress = new Uri(config["Seller:BaseUrl"]);
            });


        // Client
        builder.Services.AddScoped<ICustomerClient, CustomerClient>();
        builder.Services.AddScoped<ISellerClient, SellerClient>();

    }
}

    