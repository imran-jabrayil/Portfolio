using Portfolio.Models;

namespace Portfolio.Services.Abstractions;

public interface IWorkExperienceService
{
    Task<IEnumerable<WorkExperienceTimelineBlock>> GetWorkExperienceTimelineBlocksAsync();
}