using SorDataAPI.Models;

public interface IOrganizationService
{
    OrganizationDto? GetOrganizationBySorCode(string sorCode);

    List<OrganizationDto> GetOrganizationsByRegion(string region);
}
