using SorDataAPI.Models;

public interface IOrganizationService
{
    OrganizationDto? GetOrganizationBySorCode(string sorCode);

    Organization? GetTopLevelParentBySorCode(string sorCode);

    List<OrganizationDto> GetOrganizationsByRegion(string region);
}
