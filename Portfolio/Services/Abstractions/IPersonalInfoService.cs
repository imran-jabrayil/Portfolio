using Portfolio.Models;

namespace Portfolio.Services.Abstractions;

/// <summary>Returns the portfolio owner's personal info (always a single record).</summary>
public interface IPersonalInfoService
{
    Task<PersonalInfo?> GetPersonalInfoAsync();
}
