// -------------------------------------------------------------------------------------
//  <copyright file="ApplicationInstrumentation.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Application.Telemetry;

using System.Diagnostics;

public class ApplicationInstrumentation
{
    public static readonly string MetricsName = "ExpenseTracker.Application.Metrics";
    public static readonly string TracesName = "ExpenseTracker.Application.Traces";
    public static readonly ActivitySource ActivitySource = new(TracesName);
}