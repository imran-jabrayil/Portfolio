using FluentAssertions;
using Portfolio.Helpers;
using Portfolio.Models;
using Portfolio.UnitTests.Attributes;
using Xunit;

namespace Portfolio.UnitTests.Helpers;

public class WorkExperienceHelperTests
{
    [Fact]
    public void ConvertWorkExperienceToTimelineBlocks_EmptyList_ReturnsEmptyList()
    {
        IEnumerable<WorkExperience> workExperiences = new List<WorkExperience>();

        List<WorkExperienceTimelineBlock> result =
            WorkExperienceHelper.ConvertWorkExperienceToTimelineBlocks(workExperiences);

        result.Should().BeEmpty();
    }

    [Theory]
    [InlineAutoMoqData]
    public void ConvertWorkExperienceToTimelineBlocks_SingleWorkExperience_ReturnsSingleBlock(
        WorkExperience singleExperience)
    {
        List<WorkExperience> workExperiences = [singleExperience];

        List<WorkExperienceTimelineBlock> result =
            WorkExperienceHelper.ConvertWorkExperienceToTimelineBlocks(workExperiences);

        result.Should().HaveCount(1);
        result[0].WorkExperienceList.Should().ContainSingle();

        WorkExperience firstExp = result[0].WorkExperienceList[0];
        firstExp.Id.Should().Be(singleExperience.Id);
        firstExp.Company.Should().Be(singleExperience.Company);
        firstExp.CompanyUrl.Should().Be(singleExperience.CompanyUrl);
    }

    [Theory]
    [InlineAutoMoqData]
    public void ConvertWorkExperienceToTimelineBlocks_MultipleSameCompany_CreatesSingleBlock(
        WorkExperience exp1,
        WorkExperience exp2)
    {
        exp1.Company = exp2.Company;
        exp1.CompanyUrl = exp2.CompanyUrl;

        List<WorkExperience> workExperiences = [exp1, exp2];

        List<WorkExperienceTimelineBlock> result =
            WorkExperienceHelper.ConvertWorkExperienceToTimelineBlocks(workExperiences);

        result.Should().HaveCount(1);
        result[0].Company.Should().Be(exp1.Company);
        result[0].WorkExperienceList.Should().HaveCount(2);
        result[0].WorkExperienceList[0].Should().Be(exp1);
        result[0].WorkExperienceList[1].Should().Be(exp2);
    }

    [Theory]
    [InlineAutoMoqData]
    public void ConvertWorkExperienceToTimelineBlocks_MultipleDifferentCompanies_CreatesMultipleBlocks(
        WorkExperience exp1,
        WorkExperience exp2)
    {
        List<WorkExperience> workExperiences = [exp1, exp2];

        List<WorkExperienceTimelineBlock> result =
            WorkExperienceHelper.ConvertWorkExperienceToTimelineBlocks(workExperiences);

        result.Should().HaveCount(2);

        result[0].Company.Should().Be(exp1.Company);
        result[0].WorkExperienceList.Should().ContainSingle().Which.Should().Be(exp1);

        result[1].Company.Should().Be(exp2.Company);
        result[1].WorkExperienceList.Should().ContainSingle().Which.Should().Be(exp2);
    }

    [Theory]
    [InlineAutoMoqData]
    public void ConvertWorkExperienceToTimelineBlocks_RepeatedCompanyOutOfSequence_CreatesAdditionalBlock(
        WorkExperience exp1,
        WorkExperience exp2,
        WorkExperience exp3)
    {
        exp3.Company = exp1.Company;
        exp3.CompanyUrl = exp1.CompanyUrl;

        List<WorkExperience> workExperiences = [exp1, exp2, exp3];

        List<WorkExperienceTimelineBlock> result =
            WorkExperienceHelper.ConvertWorkExperienceToTimelineBlocks(workExperiences);

        result.Should().HaveCount(3);

        result[0].Company.Should().Be(exp1.Company);
        result[0].WorkExperienceList.Should().ContainSingle().Which.Should().Be(exp1);

        result[1].Company.Should().Be(exp2.Company);
        result[1].WorkExperienceList.Should().ContainSingle().Which.Should().Be(exp2);

        result[2].Company.Should().Be(exp3.Company);
        result[2].WorkExperienceList.Should().ContainSingle().Which.Should().Be(exp3);
    }
}