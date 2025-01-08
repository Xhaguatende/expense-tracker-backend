// -------------------------------------------------------------------------------------
//  <copyright file="Program.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

using ExpenseTracker.Api.Extensions;
using ExpenseTracker.Api.Settings;
using ExpenseTracker.Application.Extensions;
using ExpenseTracker.Infrastructure.Extensions;
using ExpenseTracker.Infrastructure.Settings;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddOptions<Auth0Settings>()
    .Bind(builder.Configuration.GetSection(nameof(Auth0Settings)));

services.AddOptions<Auth0ClientCredentialsSettings>()
    .Bind(builder.Configuration.GetSection(nameof(Auth0ClientCredentialsSettings)));

builder.Services.AddOptions<ObservabilitySettings>()
    .Bind(builder.Configuration.GetSection(nameof(ObservabilitySettings)));

builder.Services.AddOptions<MongoDbSettings>()
    .Bind(builder.Configuration.GetSection(nameof(MongoDbSettings)));

var observability = services.GetRequiredService<IOptions<ObservabilitySettings>>();
builder.AddOpenTelemetry(observability.Value);

services.RegisterApiServices();
services.RegisterInfrastructure();
services.RegisterApplication();

var app = builder.Build();

var appSettings = builder.Configuration.Get<AppSettings>();

if (appSettings is { IsSwaggerEnabled: true })
{
    app.UseOpenApi();
    app.UseSwaggerUi();
}

app.UseHttpsRedirection();

app.UseCors(opt => opt.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();
app.MapGraphQL().RequireAuthorization("GraphQLServer");

await app.RunAsync();