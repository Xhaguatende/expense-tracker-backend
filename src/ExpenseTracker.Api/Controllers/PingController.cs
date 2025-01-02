// -------------------------------------------------------------------------------------
//  <copyright file="PingController.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Api.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;

/// <summary>
/// Represents a controller for ping requests.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class PingController : ControllerBase
{
    private readonly ILogger<PingController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="PingController"/> class.
    /// </summary>
    /// <param name="logger">An instance of <see cref="ILogger{PingController}"/>.</param>
    public PingController(ILogger<PingController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Handles a ping request.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType<PingResponse>(StatusCodes.Status200OK)]
    public IActionResult Ping()
    {
        _logger.LogInformation("Starting ping...");

        return Ok(new PingResponse("Hello there!", DateTime.UtcNow));
    }

    /// <summary>
    /// Handles a ping request that requires authorization.
    /// </summary>
    /// <returns></returns>
    [HttpGet("auth")]
    [ProducesResponseType<PingResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Authorize]
    public IActionResult PingAuth()
    {
        _logger.LogInformation("Starting ping (auth)...");

        return Ok(new PingResponse("Hello there (Auth)!", DateTime.UtcNow));
    }
}