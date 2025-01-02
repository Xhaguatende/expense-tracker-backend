// -------------------------------------------------------------------------------------
//  <copyright file="ObservabilitySettings.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Infrastructure.Settings;

public class ObservabilitySettings
{
    public List<string> IgnoreRequestsForTelemetryMatching { get; set; } = [];
    public bool IsConsoleEnabled { get; set; }
    public string? OtlpEndpoint { get; set; }
    public string? OtlpHeaders { get; set; }
    public bool RecordRequestData { get; set; } = false;
    public bool RecordResponseData { get; set; } = false;
    public string? ServiceName { get; set; }
    public string? ServiceVersion { get; set; }
}