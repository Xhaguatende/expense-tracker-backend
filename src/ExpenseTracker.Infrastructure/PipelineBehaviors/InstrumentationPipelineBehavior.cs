// -------------------------------------------------------------------------------------
//  <copyright file="InstrumentationPipelineBehavior.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Infrastructure.PipelineBehaviors;

using System.Diagnostics;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using Application.Telemetry;
using HotChocolate;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Settings;

public class InstrumentationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    // ReSharper disable once StaticMemberInGenericType
    private static readonly JsonSerializerOptions _serializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        Converters = { new JsonStringEnumConverter() }
    };

    private readonly ILogger<InstrumentationPipelineBehavior<TRequest, TResponse>> _logger;
    private readonly ObservabilitySettings _observability;
    private readonly IRequestHandler<TRequest, TResponse> _requestHandler;

    public InstrumentationPipelineBehavior(
        ILogger<InstrumentationPipelineBehavior<TRequest, TResponse>> logger,
        IOptions<ObservabilitySettings> observability,
        IRequestHandler<TRequest, TResponse> requestHandler)
    {
        _logger = logger;
        _observability = observability.Value;
        _requestHandler = requestHandler;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var stopWatch = new Stopwatch();
        stopWatch.Start();

        var handlerName = _requestHandler.GetType().Name;

        using var activity = ApplicationInstrumentation.ActivitySource.StartActivity(handlerName);

        TagRequest(request, activity);

        TResponse response;

        try
        {
            response = await next();
        }
        catch (Exception ex)
        {
            TraceException(ex, activity, request);

            throw;
        }

        TagResponse(response);

        return response;
    }

    private void TagRequest(TRequest request, Activity? activity)
    {
        if (!_observability.RecordRequestData)
        {
            return;
        }

        try
        {
            activity?.AddTag(
                SettingsConstants.HandlerRequestTagName,
                JsonSerializer.Serialize(request, _serializerOptions));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to serialize request ({@TRequest}) for tagging", typeof(TRequest));
        }
    }

    private void TagResponse(TResponse response)
    {
        if (!_observability.RecordResponseData)
        {
            return;
        }

        try
        {
            Activity.Current?.AddTag(
                SettingsConstants.HandlerResponseTagName,
                response is IExecutable executable
                    ? executable.Print()
                    : JsonSerializer.Serialize(response, _serializerOptions));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to serialize response ({@TResponse}) for tagging", typeof(TResponse));
        }
    }

    private void TraceException(Exception ex, Activity? activity, TRequest request)
    {
        if (!_observability.RecordRequestData)
        {
            activity?.AddTag(
                SettingsConstants.HandlerRequestTagName,
                JsonSerializer.Serialize(request, _serializerOptions));
        }

        activity?.SetStatus(ActivityStatusCode.Error, ex.GetType().ToString());
        activity?.AddException(ex);
    }
}