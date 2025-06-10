using System.ComponentModel.DataAnnotations;
using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Portfolio.Database;
using Portfolio.Models;
using Portfolio.Services;
using Portfolio.UnitTests.Attributes;
using Portfolio.UnitTests.TestHelpers;
using Xunit;

namespace Portfolio.UnitTests.Services;

public class WorkExperienceServiceTests
{
    [Theory, InlineAutoMoqData]
    public async Task GetWorkExperiencesAsync_ReturnsOrderedWorkExperience(
        [Frozen] Mock<IDbContextFactory<PortfolioDbContext>> dbFactory,
        [Frozen] Mock<ILogger<WorkExperienceService>> logger,
        // [Range(2, 2)] WorkExperience[] workExperiences,
        IFixture fixture,
        WorkExperienceService sut)
    {
        var context = InMemoryDbContextFactory.CreateInMemoryDbContext<PortfolioDbContext>();
        
        List<WorkExperience> workExperiences = fixture.CreateMany<WorkExperience>(3).ToList(); 
        DateTime now = DateTime.Now;
        workExperiences[0].TerminationDate = now;
        workExperiences[1].TerminationDate = null;
        workExperiences[2].TerminationDate = now.AddDays(1);
        
        await context.WorkExperience.AddRangeAsync(workExperiences);
        await context.SaveChangesAsync();
        
        dbFactory.Setup(f => f.CreateDbContextAsync(CancellationToken.None))
            .ReturnsAsync(context);

        IEnumerable<WorkExperienceTimelineBlock> blocks = await sut.GetWorkExperienceTimelineBlocksAsync();
        IEnumerable<WorkExperience> actualWorkExperiences = blocks.SelectMany(block => block.WorkExperienceList);
        actualWorkExperiences.Should().Equal(workExperiences[1], workExperiences[2], workExperiences[0]);
    }
}