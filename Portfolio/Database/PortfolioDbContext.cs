using Microsoft.EntityFrameworkCore;
using Portfolio.Models;

namespace Portfolio.Database;

public class PortfolioDbContext(DbContextOptions<PortfolioDbContext> options) : DbContext(options)
{
    public DbSet<WorkExperience> WorkExperience { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WorkExperience>(workExperience =>
        {
            workExperience.HasKey(x => x.Id);
            workExperience.Property(x => x.Id)
                .ValueGeneratedOnAdd();
            
            workExperience.Property(x => x.Company)
                .IsRequired()
                .HasMaxLength(50);

            workExperience.Property(x => x.CompanyUrl)
                .IsRequired()
                .HasMaxLength(100);

            workExperience.Property(x => x.Position)
                .IsRequired()
                .HasMaxLength(50);

            workExperience.Property(x => x.Country)
                .IsRequired()
                .HasMaxLength(50);

            workExperience.Property(x => x.City)
                .IsRequired()
                .HasMaxLength(50);

            workExperience.Property(x => x.StartDate)
                .IsRequired();

            workExperience.Property(x => x.TerminationDate)
                .IsRequired(false);
        });
        
        base.OnModelCreating(modelBuilder);
    }
}
