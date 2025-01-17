namespace SorDataAPI.Models
{
    public class OrganizationDto
    {
        public required string Name { get; set; }           // Enhedsnavn
        public required string Type { get; set; }           // Enhedstype
        public required string Region { get; set; }         // P_Region
        public required string Specialty { get; set; }      // Hovedspeciale
        public required string SorCode { get; set; }        // SOR-kode
        public string? ParentSorCode { get; set; }          // Parent-SOR-kode
        public required string Cvr { get; set; }            // CVR
        public List<string>? ChildOrganizations  { get; set; } // List of child SOR codes
    }
}
