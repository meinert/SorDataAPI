namespace HealthcareAPI.Models
{
    public class Organization
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Region { get; set; }
        public string SORCode { get; set; }
        public string ParentSORCode { get; set; }
        public string CVR { get; set; }
    }
}
