using Portfolio.Models;

namespace Portfolio.Services.Abstractions;

/// <summary>Returns skills ordered by category and display order.</summary>
public interface ISkillService
{
    Task<IEnumerable<Skill>> GetSkillsAsync();
}
