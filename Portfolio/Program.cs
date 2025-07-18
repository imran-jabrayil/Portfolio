using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.EntityFrameworkCore;
using Portfolio.Database;
using Portfolio.HealthChecks;
using Portfolio.Services;
using Portfolio.Services.Abstractions;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddDataProtection()
    .PersistKeysToVault(
        new Uri(builder.Configuration.GetSection("Vault")["Uri"]!),
        builder.Configuration.GetSection("Vault")["Token"]!,
        "dataProtection",
        "portfolio")
    .UseCryptographicAlgorithms(new AuthenticatedEncryptorConfiguration
    {
        EncryptionAlgorithm = EncryptionAlgorithm.AES_256_CBC,
        ValidationAlgorithm = ValidationAlgorithm.HMACSHA256
    });

builder.Services.AddControllersWithViews();
builder.Services.AddHealthChecks()
    .AddCheck<BaseHealthCheck>("BaseHealthCheck")
    .AddCheck<DbHealthCheck<PortfolioDbContext>>("PortfolioDbHealthCheck");

builder.Services.AddDbContextFactory<PortfolioDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("Portfolio")));

builder.Services.AddScoped<IWorkExperienceService, WorkExperienceService>();

builder.Host.UseSerilog((context, config) =>
{
    config.ReadFrom.Configuration(context.Configuration);
});

WebApplication app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.MapHealthChecks("/health");

// app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Logger.LogInformation("Application started.");

app.Run();