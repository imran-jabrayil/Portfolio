using Microsoft.EntityFrameworkCore;
using Portfolio.Models;

namespace Portfolio.Database;

/// <summary>
/// Seeds the database with initial portfolio data on first run.
/// Each seed method is a no-op if the corresponding table already contains data.
/// </summary>
public static class DbInitializer
{
    public static async Task SeedAsync(IDbContextFactory<PortfolioDbContext> factory, ILogger logger)
    {
        await using PortfolioDbContext db = await factory.CreateDbContextAsync();

        await db.Database.EnsureCreatedAsync();

        await SeedPersonalInfoAsync(db, logger);
        await SeedWorkExperienceAsync(db, logger);
        await SeedSkillsAsync(db, logger);
        await SeedEducationAsync(db, logger);
        await SeedProjectsAsync(db, logger);
        await SeedAchievementsAsync(db, logger);
    }

    private static async Task SeedPersonalInfoAsync(PortfolioDbContext db, ILogger logger)
    {
        if (await db.PersonalInfo.AnyAsync()) return;

        db.PersonalInfo.Add(new PersonalInfo
        {
            Id = 1,
            Name = "Imran Jabrayilov",
            Title = "Software Engineer",
            Bio = "Backend-focused software engineer with strong expertise in .NET, Go, and cloud-native infrastructure. " +
                  "I build scalable, observable, and maintainable systems — from clean domain-driven APIs to fully automated CI/CD " +
                  "pipelines on container orchestration platforms. Passionate about distributed systems, clean architecture, and developer tooling.",
            Location = "Baku, Azerbaijan",
            GitHubUrl = "https://github.com/imran-jabrayil",
            LinkedInUrl = "https://www.linkedin.com/in/imran-jabrayilov/"
        });

        await db.SaveChangesAsync();
        logger.LogInformation("Seeded PersonalInfo.");
    }

    private static async Task SeedWorkExperienceAsync(PortfolioDbContext db, ILogger logger)
    {
        if (await db.WorkExperience.AnyAsync()) return;

        db.WorkExperience.AddRange(
            new WorkExperience
            {
                Id = 1,
                Company = "Simbrella",
                CompanyUrl = "https://www.simbrella.com/",
                Position = "Site Reliability Engineer",
                City = "Baku",
                Country = "Azerbaijan",
                StartDate = new DateTime(2024, 11, 1),
                TerminationDate = null,
                Description =
                [
                    "Prepared production environments for new projects using Nomad and Consul, ensuring seamless deployment and orchestration.",
                    "Enhanced monitoring and alerting mechanisms for critical system components using Grafana, leading to improved system observability and faster incident response.",
                    "Secured infrastructure by implementing HashiCorp Vault for secrets management, bolstering system security.",
                    "Optimized large-scale data processing workflows to reduce database load, including designing and implementing a unique ID generation mechanism to prevent duplicates and improve performance.",
                    "Collaborated with development teams to embed observability practices and enhance system performance under high-load conditions.",
                    "Built an internal drift-detection tool that periodically audits production services deployed in Nomad against source configuration in Azure DevOps — " +
                    "surfacing version mismatches, config diffs (git-format), and stale dependency versions. Includes S3-backed historical reporting and LDAP-based access control, " +
                    "with SRE-only write endpoints and read access for other teams."
                ]
            },
            new WorkExperience
            {
                Id = 2,
                Company = "Simbrella",
                CompanyUrl = "https://www.simbrella.com/",
                Position = "Software Engineer",
                City = "Baku",
                Country = "Azerbaijan",
                StartDate = new DateTime(2023, 9, 1),
                TerminationDate = new DateTime(2024, 11, 1),
                Description =
                [
                    "Contributed to the development of large-scale backend systems for mobile financial services.",
                    "Participated in end-to-end development, including backend service implementation, system integration, and deployments.",
                    "Supported the launch of a major project enabling millions of users to access mobile-based loans.",
                    "Helped improve financial inclusion through scalable and reliable FinTech infrastructure."
                ]
            },
            new WorkExperience
            {
                Id = 3,
                Company = "Huawei",
                CompanyUrl = "https://www.huawei.com/",
                Position = "Software Engineer Intern",
                City = "Hangzhou",
                Country = "China",
                StartDate = new DateTime(2023, 3, 1),
                TerminationDate = new DateTime(2023, 6, 1),
                Description =
                [
                    "Worked on performance optimization in the OpenJDK source code.",
                    "Implemented the Binary Optimization and Layout Tool (BOLT) to improve instruction layout and execution flow.",
                    "Developed call-chain clustering heuristics to optimize runtime performance.",
                    "Gained practical experience in compiler internals, low-level systems profiling, and runtime optimization."
                ]
            }
        );

        await db.SaveChangesAsync();
        logger.LogInformation("Seeded WorkExperience.");
    }

    private static async Task SeedSkillsAsync(PortfolioDbContext db, ILogger logger)
    {
        if (await db.Skills.AnyAsync()) return;

        var skills = new List<Skill>
        {
            // Languages
            new() { Id = 1,  Category = "Languages",     Name = "C#"                    },
            new() { Id = 2,  Category = "Languages",     Name = "Go"                    },
            new() { Id = 3,  Category = "Languages",     Name = "SQL"                   },
            new() { Id = 4,  Category = "Languages",     Name = "Python"                },
            new() { Id = 5,  Category = "Languages",     Name = "TypeScript"            },

            // Frameworks
            new() { Id = 6,  Category = "Frameworks",    Name = "ASP.NET Core"          },
            new() { Id = 7,  Category = "Frameworks",    Name = "Entity Framework Core" },
            new() { Id = 8,  Category = "Frameworks",    Name = "xUnit"                 },

            // Databases
            new() { Id = 9,  Category = "Databases",     Name = "SQL Server"            },
            new() { Id = 10, Category = "Databases",     Name = "PostgreSQL"            },
            new() { Id = 11, Category = "Databases",     Name = "Redis"                 },
            new() { Id = 12, Category = "Databases",     Name = "MongoDB"               },
            new() { Id = 13, Category = "Databases",     Name = "MySQL"                 },

            // DevOps
            new() { Id = 14, Category = "DevOps",        Name = "Docker"                },
            new() { Id = 15, Category = "DevOps",        Name = "GitHub Actions"        },
            new() { Id = 16, Category = "DevOps",        Name = "HashiCorp Nomad"       },
            new() { Id = 17, Category = "DevOps",        Name = "HashiCorp Vault"       },
            new() { Id = 18, Category = "DevOps",        Name = "Kubernetes"            },

            // Observability
            new() { Id = 19, Category = "Observability", Name = "Prometheus"            },
            new() { Id = 20, Category = "Observability", Name = "Grafana"               },
            new() { Id = 21, Category = "Observability", Name = "Serilog"               },
            new() { Id = 22, Category = "Observability", Name = "Grafana Loki"          },
        };

        db.Skills.AddRange(skills);
        await db.SaveChangesAsync();
        logger.LogInformation("Seeded {Count} skills.", skills.Count);
    }

    private static async Task SeedEducationAsync(PortfolioDbContext db, ILogger logger)
    {
        if (await db.Education.AnyAsync()) return;

        db.Education.Add(new Education
        {
            Id = 1,
            Institution = "University of Strasbourg",
            Degree = "Bachelor",
            FieldOfStudy = "Computer Science",
            StartYear = 2019,
            EndYear = 2023,
            Grade = "95/100",
            InstitutionUrl = "https://www.unistra.fr/",
            Description = "Program delivered through French-Azerbaijani University (UFAZ)."
        });

        await db.SaveChangesAsync();
        logger.LogInformation("Seeded Education.");
    }

    private static async Task SeedProjectsAsync(PortfolioDbContext db, ILogger logger)
    {
        if (await db.Projects.AnyAsync()) return;

        db.Projects.AddRange(
            new Project
            {
                Id = 1,
                Name = "Personal Portfolio",
                Description = "This website — a dynamic ASP.NET Core MVC portfolio deployed on a self-hosted HashiCorp Nomad cluster. " +
                              "Features Prometheus metrics, Grafana Loki structured logging, Vault-backed AES-256 data protection, " +
                              "and a full CI/CD pipeline via GitHub Actions.",
                GitHubUrl = "https://github.com/imran-jabrayil/Portfolio",
                LiveUrl = "https://jabrayilov.az",
                Technologies = "C#, ASP.NET Core, .NET 9, EF Core, SQL Server, Docker, Nomad, Vault, Prometheus, Serilog",
                IsFeatured = true
            },
            new Project
            {
                Id = 2,
                Name = "XML Encryption",
                Description = "A .NET library for AES-256-CBC XML payload encryption with HMAC-SHA256 integrity validation, " +
                              "designed for secure inter-service communication over untrusted channels.",
                Technologies = "C#, .NET, AES-256-CBC, HMACSHA256, XML",
                IsFeatured = true
            },
            new Project
            {
                Id = 3,
                Name = "HTTP Metrics Middleware",
                Description = "ASP.NET Core middleware that instruments HTTP request metrics (duration, status codes, method " +
                              "distribution) and exposes them via Prometheus for real-time Grafana dashboards.",
                Technologies = "C#, ASP.NET Core, Prometheus, Grafana",
                IsFeatured = true
            }
        );

        await db.SaveChangesAsync();
        logger.LogInformation("Seeded Projects.");
    }

    private static async Task SeedAchievementsAsync(PortfolioDbContext db, ILogger logger)
    {
        if (await db.Achievements.AnyAsync()) return;

        db.Achievements.AddRange(
            new Achievement
            {
                Id = 1,
                Title = "PASHA Bank Cup",
                Description = "Finalist",
                Years = "2023, 2024, 2025",
                Organization = "PASHA Bank"
            },
            new Achievement
            {
                Id = 2,
                Title = "Northern Eurasia Finals",
                Description = "Team",
                Years = "2022/2023",
                Organization = "ICPC"
            },
            new Achievement
            {
                Id = 3,
                Title = "Open Southern Caucasus Championship",
                Description = "Personal — High Achievement",
                Years = "2022/2023",
                Organization = "ICPC"
            },
            new Achievement
            {
                Id = 4,
                Title = "Open Southern Caucasus Championship",
                Description = "Team — 2nd degree diploma",
                Years = "2021/2022",
                Organization = "ICPC"
            },
            new Achievement
            {
                Id = 5,
                Title = "Azerbaijan Regional Contest",
                Description = "3rd place",
                Years = "2021",
                Organization = "ICPC"
            },
            new Achievement
            {
                Id = 6,
                Title = "Space Apps Challenge",
                Description = "1st place",
                Years = "2019",
                Organization = "NASA"
            },
            new Achievement
            {
                Id = 7,
                Title = "Global Scholarship Competition",
                Description = "Mathematics — 3rd place",
                Years = "2016/2017",
                Organization = "Higher School of Economics, Russia"
            },
            new Achievement
            {
                Id = 8,
                Title = "Republican Olympiads",
                Description = "2018 — Chemistry, 3rd place · 2017 — \"Sabahın alimləri\" contest, Computer Science, 3rd place · 2016 — Physics, International Olympiad Selection, 1st place",
                Years = "2016, 2017, 2018",
                Organization = "Ministry of Science and Education, Republic of Azerbaijan"
            }
        );

        await db.SaveChangesAsync();
        logger.LogInformation("Seeded {Count} achievements.", 8);
    }
}
