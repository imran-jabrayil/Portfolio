using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Portfolio.HealthChecks;

public class DbHealthCheck<TDbContext>(IDbContextFactory<TDbContext> dbContextFactory) : IHealthCheck
    where TDbContext : DbContext
{
    private readonly IDbContextFactory<TDbContext> _dbContextFactory =
        dbContextFactory ?? throw new ArgumentNullException(nameof(dbContextFactory));

    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context, 
        CancellationToken cancellationToken = new())
    {
        try
        {
            await using TDbContext dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
            await dbContext.Database.CanConnectAsync(cancellationToken);
            return HealthCheckResult.Healthy("Database is reachable");
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy(ex.Message);
        }
    }
}