using Microsoft.AspNetCore.Mvc;
using SorDataAPI.Models;
using SorDataAPI.Utilities;
using System.Linq;
using Swashbuckle.AspNetCore.Annotations;

namespace SorDataAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrganizationController : ControllerBase
    {
        /// <summary>
        /// Gets an organization by its SOR code.
        /// </summary>
        /// <param name="sorCode">The unique SOR code of the organization.</param>
        /// <returns>Returns the organization details if found.</returns>
        [HttpGet("{sorCode}")]
        [SwaggerOperation(Summary = "Get Organization by SOR Code", Description = "Fetches an organization based on its unique SOR code.")]
        [SwaggerResponse(200, "Organization found", typeof(OrganizationDto))]
        [SwaggerResponse(404, "Organization not found")]
        public IActionResult GetOrganizationBySorCode(string sorCode)
        {
            var organization = SeedData.GetTestData().FirstOrDefault(o => o.SorCode == sorCode);
            if (organization == null)
            {
                return NotFound("Organization not found");
            }

            var organizationDto = new OrganizationDto
            {
                Name = organization.Name,
                Type = organization.Type,
                Region = organization.Region,
                Specialty = organization.Specialty,
                SorCode = organization.SorCode,
                ParentSorCode = organization.ParentSorCode,
                Cvr = organization.Cvr
            };

            return Ok(organizationDto);
        }
    }
}
