namespace SorDataAPI.Models
{
    public class Organization
    {
        public required string Name { get; set; }
        public required string Type { get; set; }
        public required string Region { get; set; }
        public required string SORCode { get; set; }
        public required string ParentSORCode { get; set; }
        public required string CVR { get; set; }
    }
}
