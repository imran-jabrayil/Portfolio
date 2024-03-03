using Microsoft.EntityFrameworkCore;

namespace PortfolioWeb.Data;

public static class MigrationsExtension {
    public static void ApplyMigrations(this IApplicationBuilder app) {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        using PortfolioDbContext dbContext = scope.ServiceProvider.GetRequiredService<PortfolioDbContext>();

        dbContext.Database.Migrate();
    }
}