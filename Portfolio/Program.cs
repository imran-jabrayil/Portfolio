using Microsoft.EntityFrameworkCore;
using Portfolio.Database;
using Portfolio.HealthChecks;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddHealthChecks()
    .AddCheck<BaseHealthCheck>("BaseHealthCheck")
    .AddCheck<DbHealthCheck<PortfolioDbContext>>("PortfolioDbHealthCheck");

builder.Services.AddDbContextFactory<PortfolioDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("Portfolio")));

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

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.UseSerilogRequestLogging();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();