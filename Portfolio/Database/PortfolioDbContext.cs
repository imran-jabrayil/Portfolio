using Microsoft.EntityFrameworkCore;
using Portfolio.Models;

namespace Portfolio.Database;

/// <summary>
/// EF Core DbContext for the Portfolio application.
/// Uses IDbContextFactory pattern — never inject this directly; use IDbContextFactory&lt;PortfolioDbContext&gt;.
/// </summary>
public class PortfolioDbContext(DbContextOptions<PortfolioDbContext> options) : DbContext(options)
{
    public DbSet<WorkExperience> WorkExperience { get; set; }
    public DbSet<PersonalInfo> PersonalInfo { get; set; }
    public DbSet<Skill> Skills { get; set; }
    public DbSet<Education> Education { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Achievement> Achievements { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ConfigureWorkExperience(modelBuilder);
        ConfigurePersonalInfo(modelBuilder);
        ConfigureSkill(modelBuilder);
        ConfigureEducation(modelBuilder);
        ConfigureProject(modelBuilder);
        ConfigureAchievement(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }

    static private void ConfigureWorkExperience(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WorkExperience>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).ValueGeneratedOnAdd();
            e.Property(x => x.Company).IsRequired().HasMaxLength(50);
            e.Property(x => x.CompanyUrl).IsRequired().HasMaxLength(100);
            e.Property(x => x.Position).IsRequired().HasMaxLength(50);
            e.Property(x => x.Country).IsRequired().HasMaxLength(50);
            e.Property(x => x.City).IsRequired().HasMaxLength(50);
            e.Property(x => x.StartDate).IsRequired();
            e.Property(x => x.TerminationDate).IsRequired(false);
            // List<string> stored as a JSON array (EF Core 8+ primitive collection)
            e.Property(x => x.Description).HasColumnType("nvarchar(max)");
        });
    }

    static private void ConfigurePersonalInfo(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PersonalInfo>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).ValueGeneratedOnAdd();
            e.Property(x => x.Name).IsRequired().HasMaxLength(100);
            e.Property(x => x.Title).IsRequired().HasMaxLength(100);
            e.Property(x => x.Bio).IsRequired().HasMaxLength(1000);
            e.Property(x => x.Location).IsRequired().HasMaxLength(100);
            e.Property(x => x.Email).HasMaxLength(100);
            e.Property(x => x.GitHubUrl).HasMaxLength(200);
            e.Property(x => x.LinkedInUrl).HasMaxLength(200);
            e.Property(x => x.ResumeUrl).HasMaxLength(200);
        });
    }

    private static void ConfigureSkill(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Skill>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).ValueGeneratedOnAdd();
            e.Property(x => x.Category).IsRequired().HasMaxLength(50);
            e.Property(x => x.Name).IsRequired().HasMaxLength(50);
        });
    }

    static private void ConfigureEducation(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Education>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).ValueGeneratedOnAdd();
            e.Property(x => x.Institution).IsRequired().HasMaxLength(150);
            e.Property(x => x.Degree).IsRequired().HasMaxLength(100);
            e.Property(x => x.FieldOfStudy).IsRequired().HasMaxLength(100);
            e.Property(x => x.StartYear).IsRequired();
            e.Property(x => x.EndYear).IsRequired(false);
            e.Property(x => x.Description).HasMaxLength(500);
            e.Property(x => x.Grade).HasMaxLength(20);
            e.Property(x => x.InstitutionUrl).HasMaxLength(200);
        });
    }

    static private void ConfigureProject(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Project>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).ValueGeneratedOnAdd();
            e.Property(x => x.Name).IsRequired().HasMaxLength(100);
            e.Property(x => x.Description).IsRequired().HasMaxLength(500);
            e.Property(x => x.GitHubUrl).HasMaxLength(200);
            e.Property(x => x.LiveUrl).HasMaxLength(200);
            e.Property(x => x.Technologies).IsRequired().HasMaxLength(300);
            e.Property(x => x.IsFeatured).IsRequired();
        });
    }

    static private void ConfigureAchievement(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Achievement>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).ValueGeneratedOnAdd();
            e.Property(x => x.Title).IsRequired().HasMaxLength(150);
            e.Property(x => x.Description).IsRequired().HasMaxLength(500);
            e.Property(x => x.Years).IsRequired().HasMaxLength(30);
            e.Property(x => x.Organization).HasMaxLength(150);
        });
    }
}
