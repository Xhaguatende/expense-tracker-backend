// -------------------------------------------------------------------------------------
//  <copyright file="TelemetryFilteringProcessor.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Infrastructure.Processors;

using System.Diagnostics;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Options;
using OpenTelemetry;
using Settings;

public class TelemetryFilteringProcessor : BaseProcessor<Activity>
{
    private readonly ObservabilitySettings _observability;

    public TelemetryFilteringProcessor(IOptions<ObservabilitySettings> observability)
    {
        _observability = observability.Value;
    }

    public override void OnEnd(Activity activity)
    {
        if (IsFilteredEndpoint(activity.DisplayName)) activity.ActivityTraceFlags &= ~ActivityTraceFlags.Recorded;
    }

    private bool IsFilteredEndpoint(string displayName)
    {
        return !string.IsNullOrEmpty(displayName) &&
               _observability.IgnoreRequestsForTelemetryMatching.Exists(
                   x => Regex.IsMatch(
                       displayName,
                       x,
                       RegexOptions.None,
                       TimeSpan.FromMilliseconds(150)));
    }
}