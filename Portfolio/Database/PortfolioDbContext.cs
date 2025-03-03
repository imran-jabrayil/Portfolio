using Microsoft.EntityFrameworkCore;

namespace Portfolio.Database;

public class PortfolioDbContext(DbContextOptions<PortfolioDbContext> options) : DbContext(options);
