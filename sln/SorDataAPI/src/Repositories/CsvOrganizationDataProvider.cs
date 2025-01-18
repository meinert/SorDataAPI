using SorDataAPI.Models;

public class CsvOrganizationDataProvider : IOrganizationDataProvider
{
    private IEnumerable<Organization> _organizations;

    public IEnumerable<Organization> GetOrganizations() => _organizations;

    public void SetOrganizations(IEnumerable<Organization> organizations)
    {
        _organizations = organizations;
    }
}
