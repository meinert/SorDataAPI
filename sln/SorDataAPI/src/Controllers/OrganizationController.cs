using Microsoft.AspNetCore.Mvc;
using SorDataAPI.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace SorDataAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationService _organizationService;

        public OrganizationController(IOrganizationService organizationService)
        {
            _organizationService = organizationService;
        }

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
            var organization = _organizationService.GetOrganizationBySorCode(sorCode);
            if (organization == null) return NotFound("Organization not found");

            return Ok(organization);
        }

        /// <summary>
        /// Gets a list of organizations for a region.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <returns>Returns the organizations details if found.</returns>
        [HttpGet("{region}")]
        [SwaggerOperation(Summary = "Get Organizations by Region", Description = "Fetches all organizations for a region.")]
        [SwaggerResponse(200, "Organization(s) found", typeof(List<OrganizationDto>))]
        [SwaggerResponse(404, "Organizations not found")]
        public IActionResult GetOrganizationsByRegion(string region)
        {
            List<OrganizationDto> organizations = _organizationService.GetOrganizationsByRegion(region);
            if (organizations == null) return NotFound("Region not found");

            return Ok(organizations);
        }
    }
}
