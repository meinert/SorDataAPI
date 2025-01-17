using SorDataAPI.Models;
using SorDataAPI.Utilities;

public class OrganizationService : IOrganizationService
{
    private readonly IEnumerable<Organization> _data;

    public OrganizationService()
    {
        // Load test data (or later replace with actual data retrieval logic)
        _data = SeedData.GetTestData();
    }

    public OrganizationDto? GetOrganizationBySorCode(string sorCode)
    {
        var organization = _data.FirstOrDefault(o => o.SorCode == sorCode);
        if (organization == null) return null;

        return new OrganizationDto
        {
            Name = organization.Name,
            Type = organization.Type,
            Region = organization.Region,
            Specialty = organization.Specialty,
            SorCode = organization.SorCode,
            Cvr = organization.Cvr,
            ParentSorCode = organization.ParentSorCode,
            ChildOrganizations = [.. _data
                .Where(o => o.ParentSorCode == sorCode)
                .Select(c => new ChildOrganizationDto { SorCode = c.SorCode, Name = c.Name })]
        };
    }

    public List<OrganizationDto> GetOrganizationsByRegion(string region)
    {
        throw new NotImplementedException();
    }
}
