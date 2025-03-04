using Portfolio.Models;

namespace Portfolio.Helpers;

public static class WorkExperienceHelper
{
    public static List<WorkExperienceTimelineBlock> ConvertWorkExperienceToTimelineBlocks(IEnumerable<WorkExperience> workExperienceList)
    {
        List<WorkExperienceTimelineBlock> blocks = new();

        foreach (WorkExperience workExperience in workExperienceList)
        {
            if (blocks.Count == 0 || blocks.Last().Company != workExperience.Company)
            {
                blocks.Add(new WorkExperienceTimelineBlock
                {
                    Company = workExperience.Company,
                    CompanyUrl = workExperience.CompanyUrl,
                    WorkExperienceList = [workExperience]
                });
            }
            else
            {
                blocks.Last().WorkExperienceList.Add(workExperience);
            }
        }
        
        return blocks;
    }
}