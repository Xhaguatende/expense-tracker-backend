// -------------------------------------------------------------------------------------
//  <copyright file="ServiceCollectionExtensions.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Api.Extensions;

using System.Text.Json.Serialization;
using GraphQl.Filters;
using GraphQl.Queries.Expenses;
using Handlers;
using HotChocolate.Diagnostics;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NSwag;
using Settings;

/// <summary>
///     Provides extension methods for <see cref="IServiceCollection" />.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Gets the required service.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the service.
    /// </typeparam>
    /// <param name="services">The service collection</param>
    /// <returns>An instance of <see cref="IServiceCollection"/>.</returns>
    public static T GetRequiredService<T>(this IServiceCollection services) where T : class
    {
        var serviceProvider = services.BuildServiceProvider();

        var options = serviceProvider.GetRequiredService<T>();

        return options;
    }

    /// <summary>
    /// Registers the API services.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The services.</returns>
    public static IServiceCollection RegisterApiServices(this IServiceCollection services)
    {
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();

        services.AddControllers();
        services.AddGraphQlServer();

        services.AddOpenApiDocument();

        services.AddAuth();

        return services;
    }

    private static void AddAuth(this IServiceCollection services)
    {
        services.AddAuthentication(
                options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
            .AddJwtBearer(
                options =>
                {
                    var auth0Settings = services.GetRequiredService<IOptions<Auth0Settings>>().Value;
                    var issuer = $"https://{auth0Settings.Domain}/";
                    options.Authority = issuer;
                    options.Audience = auth0Settings.Audience;

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = issuer,
                        ValidateAudience = true,
                        ValidAudience = auth0Settings.Audience,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromMinutes(1)
                    };
                });

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>(
                "BasicAuthenticationSchemaName",
                null);

        services.AddAuthorization();

        services.AddAuthorizationBuilder()
            .AddPolicy(
                "GraphQLServer",
                policy =>
                {
                    policy.AddAuthenticationSchemes(
                        JwtBearerDefaults.AuthenticationScheme,
                        "BasicAuthenticationSchemaName");

                    policy.RequireAuthenticatedUser().Build();
                });
    }

    private static void AddControllers(this IServiceCollection services)
    {
        services.AddControllersWithViews(
                options => { options.SuppressAsyncSuffixInActionNames = false; })
            .ConfigureApiBehaviorOptions(
                options => { options.SuppressModelStateInvalidFilter = true; })
            .AddJsonOptions(
                options =>
                {
                    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

        services.AddRouting(
            options => { options.LowercaseUrls = true; });
    }

    private static void AddGraphQlServer(this IServiceCollection services)
    {
        services.AddGraphQLServer()
            .AddApiTypes()
            .ModifyCostOptions(o => o.EnforceCostLimits = false)
            .AddMongoDbFiltering()
            .AddMongoDbSorting()
            .AddMongoDbProjections()
            .AddMongoDbPagingProviders()
            .AddAuthorization()
            .AddInstrumentation(
                o =>
                {
                    o.RequestDetails = RequestDetails.All;
                    o.Scopes = ActivityScopes.ExecuteRequest;
                })
            .AddMutationConventions(applyToAllMutations: false)
            .AddTypeExtension<ExpensesQuery>()
            .InitializeOnStartup();

        services.AddErrorFilter<HotChocolateErrorFilter>();
    }

    private static void AddOpenApiDocument(this IServiceCollection services)
    {
        services.AddOpenApiDocument(
            options =>
            {
                options.PostProcess = document =>
                {
                    document.Info = new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "Expense Tracker API",
                        Description = "Expense Tracker Web API",
                    };
                };
            });
    }
}