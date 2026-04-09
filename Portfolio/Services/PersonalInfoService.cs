using Microsoft.EntityFrameworkCore;
using Portfolio.Database;
using Portfolio.Models;
using Portfolio.Services.Abstractions;

namespace Portfolio.Services;

/// <summary>Retrieves the single PersonalInfo record from the database.</summary>
public class PersonalInfoService(
    ILogger<PersonalInfoService> logger,
    IDbContextFactory<PortfolioDbContext> dbContextFactory) : IPersonalInfoService
{
    private readonly ILogger<PersonalInfoService> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IDbContextFactory<PortfolioDbContext> _dbContextFactory =
        dbContextFactory ?? throw new ArgumentNullException(nameof(dbContextFactory));

    /// <inheritdoc/>
    public async Task<PersonalInfo?> GetPersonalInfoAsync()
    {
        await using PortfolioDbContext db = await _dbContextFactory.CreateDbContextAsync();
        PersonalInfo? info = await db.PersonalInfo.FirstOrDefaultAsync();

        if (info is null)
            _logger.LogWarning("No PersonalInfo record found in the database.");

        return info;
    }
}
