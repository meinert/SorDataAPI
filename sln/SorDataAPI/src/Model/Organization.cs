using CsvHelper.Configuration.Attributes;

namespace SorDataAPI.Models
{
    public class Organization
    {
        [Name("Enhedsnavn")]
        public required string Name { get; set; }           // Enhedsnavn
        
        [Name("Enhedstype")]
        public required TypeEnum Type { get; set; }         // Enhedstype
        
        [Name("P_Region")]
        public required string Region { get; set; }         // P_Region
        
        [Name("Hovedspeciale")]
        public required string Specialty { get; set; }      // Hovedspeciale
        
        [Name("SOR-kode")]
        public required string SorCode { get; set; }        // SOR-kode
        
        [Name("Parent-SOR-kode")]
        public string? ParentSorCode { get; set; }          // Parent-SOR-kode
        
        [Name("CVR")]
        public required string Cvr { get; set; }            // CVR
    }
}
