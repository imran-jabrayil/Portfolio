﻿using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Portfolio.HealthChecks;

public class BaseHealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = new()) =>
        Task.FromResult(HealthCheckResult.Healthy("Checking health check"));
}