using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Moq;
using Portfolio.Database;
using Portfolio.HealthChecks;
using Portfolio.UnitTests.Attributes;
using Xunit;

namespace Portfolio.UnitTests.HealthChecks;

public class DbHealthCheckTests
{
    [Theory, InlineAutoMoqData]
    public async Task CheckHealthAsync_ConnectedToDatabase_HealthyReturned(
        Mock<IDbContextFactory<PortfolioDbContext>> dbContextFactory,
        Mock<PortfolioDbContext> dbContext)
    {
        dbContextFactory.Setup(factory => 
                factory.CreateDbContextAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(dbContext.Object);
        
        Mock<DatabaseFacade> dbFacade = new Mock<DatabaseFacade>(dbContext.Object);
        dbFacade.Setup(facade => facade.CanConnectAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);
        
        dbContext.Setup(context => context.Database)
            .Returns(dbFacade.Object);

        DbHealthCheck<PortfolioDbContext> check = new(dbContextFactory.Object);

        HealthCheckResult result = await check.CheckHealthAsync(new HealthCheckContext());
        
        result.Status.Should().Be(HealthStatus.Healthy);
        result.Description.Should().Be("Database is reachable");
    }
    
    [Theory, InlineAutoMoqData]
    public async Task CheckHealthAsync_NotConnectedToDatabase_HealthyReturned(
        Mock<IDbContextFactory<PortfolioDbContext>> dbContextFactory,
        Mock<PortfolioDbContext> dbContext)
    {
        dbContextFactory.Setup(factory => 
                factory.CreateDbContextAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(dbContext.Object);
        
        Mock<DatabaseFacade> dbFacade = new Mock<DatabaseFacade>(dbContext.Object);
        dbFacade.Setup(facade => facade.CanConnectAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);
        
        dbContext.Setup(context => context.Database)
            .Returns(dbFacade.Object);

        DbHealthCheck<PortfolioDbContext> check = new(dbContextFactory.Object);

        HealthCheckResult result = await check.CheckHealthAsync(new HealthCheckContext());
        
        result.Status.Should().Be(HealthStatus.Unhealthy);
        result.Description.Should().Be("Could not connect to database");
    }
    
    [Theory, InlineAutoMoqData]
    public async Task CheckHealthAsync_NotConnectedToDatabase_ExceptionThrown_UnhealthyReturned(
        Mock<IDbContextFactory<PortfolioDbContext>> dbContextFactory,
        Mock<PortfolioDbContext> dbContext,
        string errorMessage)
    {
        dbContextFactory.Setup(factory => 
                factory.CreateDbContextAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(dbContext.Object);
        
        Mock<DatabaseFacade> dbFacade = new Mock<DatabaseFacade>(dbContext.Object);
        dbFacade.Setup(facade => facade.CanConnectAsync(It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception(errorMessage));
        
        dbContext.Setup(context => context.Database)
            .Returns(dbFacade.Object);

        DbHealthCheck<PortfolioDbContext> check = new(dbContextFactory.Object);

        HealthCheckResult result = await check.CheckHealthAsync(new HealthCheckContext());
        
        result.Status.Should().Be(HealthStatus.Unhealthy);
        result.Description.Should().Be(errorMessage);
    }
}