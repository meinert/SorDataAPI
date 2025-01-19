namespace SorDataAPI.Models
{
    /// <summary>
    /// Represents the data transfer object (DTO) for an organization.
    /// Provides a lightweight model for transferring organization data in API responses.
    /// </summary>
    public class OrganizationDto
    {
        public required string Name { get; set; }           // Enhedsnavn
        public required TypeEnum Type { get; set; }         // Enhedstype
        public required string Region { get; set; }         // P_Region
        public required string Specialty { get; set; }      // Hovedspeciale
        public required string SorCode { get; set; }        // SOR-kode
        public string? ParentSorCode { get; set; }          // Parent-SOR-kode
        public required string Cvr { get; set; }            // CVR
        public List<ChildOrganizationDto>? ChildOrganizations { get; set; } // List of child SOR codes

        /// <summary>
        /// Static factory method to create an OrganizationDto from an Organization and a list of child organizations.
        /// </summary>
        public static OrganizationDto FromOrganization(Organization organization, List<Organization> childOrganizations)
        {
            return new OrganizationDto
            {
                Name = organization.Name,
                Type = organization.Type,
                Region = organization.Region,
                Specialty = organization.Specialty,
                SorCode = organization.SorCode,
                ParentSorCode = organization.ParentSorCode,
                Cvr = organization.Cvr,
                ChildOrganizations = [.. childOrganizations.Select(c => ChildOrganizationDto.FromOrganization(c))]
            };
        }
    }
}
