using SorDataAPI.Models;

/// <summary>
/// Defines the contract for a data provider that manages a collection of organizations.
/// </summary>
public interface IOrganizationDataProvider
{
    IEnumerable<Organization>? GetOrganizations();

    void SetOrganizations(IEnumerable<Organization> organizations);
}
