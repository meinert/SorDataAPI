using SorDataAPI.Models;
using System.Collections.Generic;

namespace SorDataAPI.Utilities
{
    public static class SeedData
    {
        public static List<Organization> GetTestData()
        {
            return new List<Organization>
            {
                new Organization
                {
                    Name = "Organization A",
                    Type = "Private",
                    Region = "Region 1",
                    Specialty = "Cardiology",
                    SorCode = "12345",
                    ParentSorCode = null,
                    Cvr = "1234567890"
                },
                new Organization
                {
                    Name = "Child Org A1",
                    Type = "Municipality",
                    Region = "Region 1",
                    Specialty = "Neurology",
                    SorCode = "12346",
                    ParentSorCode = "12345",
                    Cvr = "1234567891"
                },
                new Organization
                {
                    Name = "Child Org A1.1",
                    Type = "Private",
                    Region = "Region 1",
                    Specialty = "Orthopedics",
                    SorCode = "12347",
                    ParentSorCode = "12346",
                    Cvr = "1234567892"
                },
                new Organization
                {
                    Name = "Organization B",
                    Type = "Public",
                    Region = "Region 2",
                    Specialty = "Pediatrics",
                    SorCode = "22345",
                    ParentSorCode = null,
                    Cvr = "2234567890"
                }
            };
        }
    }
}
