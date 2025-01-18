using SorDataAPI.Models;
using SorDataAPI.Utilities;

public class OrganizationService : IOrganizationService
{
    private readonly IOrganizationDataProvider _dataProvider;
    private List<Organization> _organizationList;

    public OrganizationService(IOrganizationDataProvider dataProvider)
    {
        _dataProvider = dataProvider;
        _organizationList = _dataProvider.GetOrganizations().ToList();
    }

    // Method to initialize the data (set organizations after parsing)
    public void InitializeData(IEnumerable<Organization> organizations)
    {
        _dataProvider.SetOrganizations(organizations);
        _organizationList = _dataProvider.GetOrganizations().ToList();
    }

    public OrganizationDto? GetOrganizationBySorCode(string sorCode)
    {
        var organization = _organizationList.FirstOrDefault(o => o.SorCode == sorCode);
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
            ChildOrganizations = [.. _organizationList
                .Where(o => o.ParentSorCode == sorCode)
                .Select(c => new ChildOrganizationDto { SorCode = c.SorCode, Name = c.Name })]
        };
    }

    public Organization? GetTopLevelParentBySorCode(string sorCode)
    {
        List<Organization> organization = _organizationList.FindAll(o => o.SorCode == sorCode);

        if (organization.Count != 1) return null;
        
        if (organization == null) return null;

        Organization parent_organization = organization[0];

        // Traverse upwards until we reach the top-level parent
        while (parent_organization.ParentSorCode != "")
        {
            parent_organization = _organizationList.FirstOrDefault(o => o.SorCode == parent_organization.ParentSorCode);
            if (parent_organization == null)
                break; // If no parent is found, exit the loop
        }

        return parent_organization; // This will be the top-level parent
    }
    public List<OrganizationDto> GetOrganizationsByRegion(string region)
    {
        throw new NotImplementedException();
    }
}
