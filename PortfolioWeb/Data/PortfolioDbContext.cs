using Microsoft.EntityFrameworkCore;

namespace PortfolioWeb.Data;

public sealed class PortfolioDbContext(DbContextOptions<PortfolioDbContext> options) : DbContext(options);
