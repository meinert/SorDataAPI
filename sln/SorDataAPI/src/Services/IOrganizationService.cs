using SorDataAPI.Models;

/// <summary>
/// Provides methods to manage and retrieve organization data in a structured manner.
/// </summary>
public interface IOrganizationService
{
    OrganizationDto? GetOrganizationBySorCode(string sorCode);

    OrganizationDto? GetTopLevelParentBySorCode(string sorCode);

    List<OrganizationDto> GetOrganizationsByRegion(string region);
}
