using SorDataAPI.Models;

namespace SorDataAPI.Utilities
{
    /// <summary>
    /// Used for testing
    /// </summary>
    public static class SeedData
    {
        public static List<Organization> GetTestData()
        {
            return
            [
                new Organization
                {
                    Name = "Organization A",
                    Type = (TypeEnum) Enum.Parse(typeof(TypeEnum), "Private".ToUpperInvariant()),
                    Region = "Region 1",
                    Specialty = "Cardiology",
                    SorCode = "12345",
                    ParentSorCode = null,
                    Cvr = "1234567890"
                },
                new Organization
                {
                    Name = "Child Org A1",
                    Type = (TypeEnum) Enum.Parse(typeof(TypeEnum), "Municipality".ToUpperInvariant()), // "Municipality",
                    Region = "Region 1",
                    Specialty = "Neurology",
                    SorCode = "12346",
                    ParentSorCode = "12345",
                    Cvr = "1234567891"
                },
                new Organization
                {
                    Name = "Child Org A1.1",
                    Type = (TypeEnum) Enum.Parse(typeof(TypeEnum), "Private".ToUpperInvariant()),
                    Region = "Region 1",
                    Specialty = "Orthopedics",
                    SorCode = "12347",
                    ParentSorCode = "12346",
                    Cvr = "1234567892"
                },
                new Organization
                {
                    Name = "Organization B",
                    Type = (TypeEnum) Enum.Parse(typeof(TypeEnum), "Public".ToUpperInvariant()), //"Public",
                    Region = "Region 2",
                    Specialty = "Pediatrics",
                    SorCode = "22345",
                    ParentSorCode = null,
                    Cvr = "2234567890"
                }
            ];
        }
    }
}
