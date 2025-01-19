namespace SorDataAPI.Models
{
    /// <summary>
    /// Dto used in <see cref="OrganizationDto"/> instead of reusing OrganizationDto to avoid over fetching data
    /// </summary>
    public class ChildOrganizationDto
    {
        public required string Name { get; set; }           // Enhedsnavn
        public required string SorCode { get; set; }        // SOR-kode

        /// <summary>
        /// Static factory method to create a ChildOrganizationDto from an Organization.
        /// </summary>
        public static ChildOrganizationDto FromOrganization(Organization organization)
        {
            return new ChildOrganizationDto
            {
                Name = organization.Name,
                SorCode = organization.SorCode
            };
        }
    }
}
