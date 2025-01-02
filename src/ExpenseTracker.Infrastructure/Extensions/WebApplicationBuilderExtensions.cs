// -------------------------------------------------------------------------------------
//  <copyright file="WebApplicationBuilderExtensions.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Infrastructure.Extensions;

using Application.Telemetry;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Processors;
using Settings;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddOpenTelemetry(
        this WebApplicationBuilder builder,
        ObservabilitySettings observability)
    {
        var attributes = new Dictionary<string, object>
        {
            // See https://github.com/open-telemetry/opentelemetry-specification/tree/main/specification/resource/semantic_conventions
            ["host.name"] = Environment.MachineName,
            ["os.description"] = System.Runtime.InteropServices.RuntimeInformation.OSDescription,
            ["deployment.environment"] = builder.Environment.EnvironmentName.ToLowerInvariant()
        };

        var entryAssembly = System.Reflection.Assembly.GetEntryAssembly();
        var entryAssemblyName = entryAssembly?.GetName();
        var versionAttribute = entryAssembly?.GetCustomAttributes(false)
            .OfType<System.Reflection.AssemblyInformationalVersionAttribute>()
            .FirstOrDefault();

        var serviceName = observability.ServiceName ?? entryAssemblyName?.Name ?? "UnknownServiceName";
        var serviceVersion = observability.ServiceVersion ??
                             versionAttribute?.InformationalVersion ??
                             entryAssemblyName?.Version?.ToString() ?? "UnknownServiceVersion";

        var resourceBuilder = ResourceBuilder.CreateDefault()
            .AddService(serviceName, serviceVersion: serviceVersion)
            .AddTelemetrySdk()
            .AddAttributes(attributes);

        builder.Logging.ClearProviders()
            .AddOpenTelemetry(
                options =>
                {
                    options.IncludeFormattedMessage = true;
                    options.IncludeScopes = true;
                    options.ParseStateValues = true;
                    options.SetResourceBuilder(resourceBuilder);

                    VerifyConsoleExporterLogging(observability, options);

                    VerifyOtlpLogging(observability, options);
                })
            .AddConfiguration(builder.Configuration);

        builder.Services.AddOpenTelemetry()
            .WithTracing(
                b =>
                {
                    b.AddSource(ApplicationInstrumentation.TracesName);
                    b.AddProcessor<TelemetryFilteringProcessor>();
                    b.SetResourceBuilder(resourceBuilder);
                    b.AddHttpClientInstrumentation();
                    b.AddHotChocolateInstrumentation();
                    b.AddAspNetCoreInstrumentation(opt => { opt.RecordException = true; });

                    b.AddMongoDbInstrumentation();

                    VerifyConsoleExporterTracing(observability, b);

                    VerifyOtlpTracing(observability, b);
                });

        builder.Services.AddOpenTelemetry()
            .WithMetrics(
                b =>
                {
                    b.AddMeter(
                        "Microsoft.AspNetCore.Hosting",
                        "System.Net.Http",
                        ApplicationInstrumentation.MetricsName);

                    b.SetResourceBuilder(resourceBuilder);

                    b.AddAspNetCoreInstrumentation();
                    b.AddRuntimeInstrumentation();

                    VerifyConsoleExporterMetrics(observability, b);

                    VerifyOtlpMetrics(observability, b);
                });

        return builder;
    }

    private static void AddMongoDbInstrumentation(this TracerProviderBuilder builder)
    {
        builder.AddSource("MongoDB.Driver.Core.Extensions.DiagnosticSources");
    }

    private static void VerifyConsoleExporterLogging(ObservabilitySettings observability, OpenTelemetryLoggerOptions options)
    {
        if (observability.IsConsoleEnabled)
        {
            options.AddConsoleExporter();
        }
    }

    private static void VerifyConsoleExporterMetrics(ObservabilitySettings observability, MeterProviderBuilder builder)
    {
        if (observability.IsConsoleEnabled)
        {
            builder.AddConsoleExporter();
        }
    }

    private static void VerifyConsoleExporterTracing(
        ObservabilitySettings observability,
        TracerProviderBuilder tracerProviderBuilder)
    {
        if (observability.IsConsoleEnabled)
        {
            tracerProviderBuilder.AddConsoleExporter();
        }
    }

    private static void VerifyOtlpLogging(ObservabilitySettings observability, OpenTelemetryLoggerOptions options)
    {
        if (!string.IsNullOrWhiteSpace(observability.OtlpEndpoint))
        {
            options.AddOtlpExporter(
                o =>
                {
                    o.Endpoint = new Uri(observability.OtlpEndpoint);

                    if (!string.IsNullOrWhiteSpace(observability.OtlpHeaders))
                    {
                        o.Headers = observability.OtlpHeaders;
                    }
                });
        }
    }

    private static void VerifyOtlpMetrics(ObservabilitySettings observability, MeterProviderBuilder builder)
    {
        if (!string.IsNullOrWhiteSpace(observability.OtlpEndpoint))
        {
            builder.AddOtlpExporter(
                options =>
                {
                    options.Endpoint = new Uri(observability.OtlpEndpoint);

                    if (!string.IsNullOrWhiteSpace(observability.OtlpHeaders))
                    {
                        options.Headers = observability.OtlpHeaders;
                    }
                });
        }
    }

    private static void VerifyOtlpTracing(ObservabilitySettings observability, TracerProviderBuilder tracerProviderBuilder)
    {
        if (!string.IsNullOrWhiteSpace(observability.OtlpEndpoint))
        {
            tracerProviderBuilder.AddOtlpExporter(
                options =>
                {
                    options.Endpoint = new Uri(observability.OtlpEndpoint);

                    if (!string.IsNullOrWhiteSpace(observability.OtlpHeaders))
                    {
                        options.Headers = observability.OtlpHeaders;
                    }
                });
        }
    }
}