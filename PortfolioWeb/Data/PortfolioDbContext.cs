using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PortfolioWeb.Models;

namespace PortfolioWeb.Data;

public sealed class PortfolioDbContext(DbContextOptions<PortfolioDbContext> options) : IdentityDbContext(options) {
    public DbSet<City> Cities { get; set; } = null!;
    public DbSet<Company> Companies { get; set; } = null!;
    public DbSet<Country> Countries { get; set; } = null!;
    public DbSet<Position> Positions { get; set; } = null!;
    public DbSet<WorkExperience> WorkExperiences { get; set; } = null!;


    protected override void OnModelCreating(ModelBuilder builder) {
        builder.Entity<Country>(country => {
            country.HasKey(c => c.Code);
            country.Property(c => c.Code)
                .HasMaxLength(3);
            country.Property(c => c.Name)
                .HasMaxLength(40);
            country.HasData(
                new Country { Code = "AZE", Name = "Azerbaijan" },
                new Country { Code = "CHN", Name = "China" });
        });

        builder.Entity<City>(city => {
            city.HasKey(c => c.Name);
            city.Property(c => c.Name)
                .HasMaxLength(30);
            city.HasOne(c => c.Country)
                .WithMany(c => c.Cities);
            city.HasData(
                new City { Name = "Baku", CountryCode = "AZE" },
                new City { Name = "Hangzhou", CountryCode = "CHN" });
        });

        builder.Entity<Company>(company => {
            company.HasKey(c => c.Name);
            company.Property(c => c.Name)
                .HasMaxLength(40);
            company.HasData(
                new Company { Name = "Simbrella", WebsiteUrl = "https://www.simbrella.com/" },
                new Company { Name = "Huawei", WebsiteUrl = "https://www.huawei.com/" });
        });

        builder.Entity<Position>(position => {
            position.HasKey(p => p.Name);
            position.Property(p => p.Name)
                .HasMaxLength(30);
            position.HasData(
                new Position { Name = "Software Engineer" });
        });

        builder.Entity<WorkExperience>(workExperience => {
            workExperience.HasKey(we => we.Id);
            workExperience.Property(we => we.CityName)
                .HasMaxLength(30);
            workExperience.Property(we => we.CompanyName)
                .HasMaxLength(40);
            workExperience.Property(we => we.PositionName)
                .HasMaxLength(30);

            workExperience.HasData(
                new WorkExperience {
                    Id = 1,
                    CityName = "Hangzhou",
                    CompanyName = "Huawei",
                    PositionName = "Software Engineer",
                    Level = Level.Intern,
                    StartDate = new DateTime(2023, 3, 27),
                    TerminationDate = new DateTime(2023, 6, 20) }, 
                new WorkExperience {
                    Id = 2,
                    CityName = "Baku",
                    CompanyName = "Simbrella",
                    PositionName = "Software Engineer",
                    Level = Level.Middle,
                    StartDate = new DateTime(2023, 9, 11),
                    TerminationDate = null });
        });
        
        base.OnModelCreating(builder);
    }
}
