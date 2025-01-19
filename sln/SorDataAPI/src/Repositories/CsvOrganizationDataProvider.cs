using SorDataAPI.Models;

/// <summary>
/// Provides access to organization data sourced from a CSV file.
/// This class implements the <see cref="IOrganizationDataProvider"/> interface
/// and is used to store and retrieve the parsed organization data.
/// </summary>
public class CsvOrganizationDataProvider : IOrganizationDataProvider
{
    // Holds the organization data in memory
    private IEnumerable<Organization>? _organizations;

    /// <summary>
    /// Retrieves the current collection of organizations.
    /// </summary>
    /// <returns>
    /// An <see cref="IEnumerable{Organization}"/> containing the current organizations,
    /// or <c>null</c> if no organizations have been set.
    /// </returns>
    public IEnumerable<Organization>? GetOrganizations()
    {
        return _organizations;
    }

    /// <summary>
    /// Sets the collection of organizations to be managed by this provider.
    /// </summary>
    /// <param name="organizations">
    /// An <see cref="IEnumerable{Organization}"/> containing the organizations to be stored.
    /// </param>
    public void SetOrganizations(IEnumerable<Organization> organizations)
    {
        _organizations = organizations;
    }
}
