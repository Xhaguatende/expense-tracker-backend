// -------------------------------------------------------------------------------------
//  <copyright file="ServiceCollectionExtensions.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Infrastructure.Extensions;

using Application.Repositories;
using Application.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using MongoDB.Driver.Core.Extensions.DiagnosticSources;
using PipelineBehaviors;
using Repositories;
using Services;
using Settings;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterInfrastructure(this IServiceCollection services)
    {
        services.AddRepositories();

        services.AddMediator();

        services.AddServices();

        return services;
    }

    private static void AddMediator(this IServiceCollection services)
    {
        var infrastructureAssembly = typeof(IAssemblyReference).Assembly;

        services.AddMediatR(
            cfg =>
            {
                cfg.RegisterServicesFromAssembly(infrastructureAssembly);
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(InstrumentationPipelineBehavior<,>));
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
            });
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        var mongoDbSettings = services.GetRequiredService<IOptions<MongoDbSettings>>().Value;

        services
            .AddSingleton<IMongoClient>(
                sp =>
                {
                    var loggerFactory = sp.GetRequiredService<ILoggerFactory>();
                    var settings = MongoClientSettings.FromConnectionString(mongoDbSettings.ConnectionString);
                    settings.ClusterConfigurator = cb => cb.Subscribe(new DiagnosticsActivityEventSubscriber());
                    settings.LoggingSettings = new LoggingSettings(loggerFactory, 10000);

                    return new MongoClient(settings);
                });

        services.AddSingleton(
            sp =>
            {
                var mongoClient = sp.GetRequiredService<IMongoClient>();
                var mongoDatabase = mongoClient.GetDatabase(mongoDbSettings.DatabaseName);

                return mongoDatabase;
            });

        services.AddScoped<IExpenseRepository, ExpenseRepository>();
        services.AddScoped<IExpenseViewRepository, ExpenseViewRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICurrencyRepository, CurrencyRepository>();

        MongoConventions.Initialize();
    }

    private static void AddServices(this IServiceCollection services)
    {
        services.AddHttpContextAccessor()
            .AddSingleton<IApplicationContextService, HttpApplicationContextService>();


        services.AddHttpClient<IAuthService, AuthService>((serviceProvider, client) =>
        {
            var settings = serviceProvider
                .GetRequiredService<IOptions<Auth0ClientCredentialsSettings>>().Value;

            client.BaseAddress = new Uri(settings.BaseUrl);
        });
    }

    private static T GetRequiredService<T>(this IServiceCollection services) where T : class
    {
        var serviceProvider = services.BuildServiceProvider();

        var options = serviceProvider.GetRequiredService<T>();

        return options;
    }
}