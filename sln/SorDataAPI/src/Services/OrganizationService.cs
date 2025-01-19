using SorDataAPI.Models;

public class OrganizationService : IOrganizationService
{
    private readonly IOrganizationDataProvider _dataProvider;
    private List<Organization> _organizationList;

    public OrganizationService(IOrganizationDataProvider dataProvider)
    {
        _dataProvider = dataProvider ?? throw new ArgumentNullException(nameof(dataProvider));

        var organizations = _dataProvider.GetOrganizations()
        ?? throw new ArgumentException("No organizations found in data provider");

        _organizationList = [.. organizations];
    }

    public OrganizationDto? GetOrganizationBySorCode(string sorCode)
    {
        Organization? organization = _organizationList.FirstOrDefault(o => o.SorCode == sorCode);
        return buildOrganizationDto(organization);
    }

    public OrganizationDto? GetTopLevelParentBySorCode(string sorCode)
    {
        List<Organization> organizationsBySorCode = _organizationList.FindAll(o => o.SorCode == sorCode);
        if (organizationsBySorCode == null)
        {
            return null;
        }
        else if (organizationsBySorCode.Count != 1)
        {
            Console.WriteLine("More than one organization found with the SOR code: " + sorCode);
            return null;
        }

        Organization organization = organizationsBySorCode.First();       
        Organization? topParentOrganization = FindTopParentOrganization(organization);

        return buildOrganizationDto(topParentOrganization);
    }

    public List<OrganizationDto> GetOrganizationsByRegion(string region)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Builds an OrganizationDto from an Organization and its child organizations.
    /// </summary>
    /// <param name="organization">Organization to build the OrganizationDto from</param>
    /// <returns>OrganizationDto. Otherwise null if the organization is null</returns>
    private OrganizationDto? buildOrganizationDto(Organization? organization)
    {
        if (organization == null)
            return null;

        // Find all child organizations for the given organization and construct the OrganizationDto
        List<Organization> childOrganizations = _organizationList.FindAll(o => o.ParentSorCode == organization.SorCode);
        return OrganizationDto.FromOrganization(organization, childOrganizations);
    }

    /// <summary>
    /// Recursively finds the top parent organization for the given organization.
    /// </summary>
    /// <param name="organization">Organization to find the top parent for</param>
    /// <returns>Top parent organization. Otherwise null if an organization could not be found</returns>
    private Organization? FindTopParentOrganization(Organization organization)
    {
        string? parentSorCode = organization.ParentSorCode;
        // The top parent organization is found
        if (string.IsNullOrEmpty(parentSorCode)) return organization;

        Organization? parentOrganization = _organizationList.FirstOrDefault(o => o.SorCode == parentSorCode);
        if (parentOrganization == null)
        {
            Console.WriteLine("Parent organization not found for SOR code: " + parentSorCode);
            return null;
        }

        return FindTopParentOrganization(parentOrganization);
    }
}
