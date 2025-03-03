using Microsoft.EntityFrameworkCore;

namespace Portfolio.Database;

public sealed class PortfolioDbContext(DbContextOptions<PortfolioDbContext> options) : DbContext(options);
