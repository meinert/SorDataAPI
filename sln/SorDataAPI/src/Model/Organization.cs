namespace SorDataAPI.Models
{
    public class Organization
    {
        public required string Name { get; set; }           // Enhedsnavn
        public required string Type { get; set; }           // Enhedstype
        public required string Region { get; set; }         // P_Region
        public required string Specialty { get; set; }      // Hovedspeciale
        public required string SorCode { get; set; }        // SOR-kode
        public string? ParentSorCode { get; set; }          // Parent-SOR-kode
        public required string Cvr { get; set; }            // CVR
    }
}
