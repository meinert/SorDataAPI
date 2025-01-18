using SorDataAPI.Models;

public interface IOrganizationDataProvider
{
    IEnumerable<Organization> GetOrganizations();
    void SetOrganizations(IEnumerable<Organization> organizations);
}
