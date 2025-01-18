using System.Buffers.Text;
using Microsoft.AspNetCore.Mvc;
using SorDataAPI.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace SorDataAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OrganizationController : ControllerBase
    {
        private const string GET_BY_SOR = "sor/{sorCode}";
        private const string TRAVERSE_BY_SOR = "sor/traverse/{sorCode}";
        private const string GET_BY_REGION = "region/{region}";


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
        [HttpGet(GET_BY_SOR)]
        [SwaggerOperation(Summary = "Get Organization by SOR Code", Description = "Fetches an organization based on its unique SOR code.")]
        [SwaggerResponse(200, "Organization found", typeof(OrganizationDto))]
        [SwaggerResponse(404, "Organization not found")]
        public ActionResult<Organization> GetOrganizationBySorCode(string sorCode)
        {
            var organization = _organizationService.GetOrganizationBySorCode(sorCode);
            if (organization == null) return NotFound("Organization not found");

            return Ok(organization);
        }

        /// <summary>
        /// Traverses the organization hierarchy upwards to find the top-level parent organization.
        /// </summary>
        /// <param name="sorCode">The SOR code of the organization to start the traversal.</param>
        /// <returns>The top-level parent organization.</returns>
        [HttpGet(TRAVERSE_BY_SOR)]
        public ActionResult<Organization> GetTopLevelParent(string sorCode)
        {
            var parentOrganization = _organizationService.GetTopLevelParentBySorCode(sorCode);

            if (parentOrganization == null)
            {
                return NotFound($"Organization with SOR code {sorCode} not found.");
            }

            return Ok(parentOrganization);
        }

        /// <summary>
        /// Gets a list of organizations for a region.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <returns>Returns the organizations details if found.</returns>
        /// <remarks>
        /// This method is not yet implemented. It will be implemented in a future release.
        /// </remarks>
        [HttpGet(GET_BY_REGION)]
        [SwaggerOperation(Summary = "Get Organizations by Region", Description = "Fetches all organizations for a region.")]
        [SwaggerResponse(200, "Organization(s) found", typeof(List<OrganizationDto>))]
        [SwaggerResponse(404, "Organizations not found")]
        public ActionResult<Organization> GetOrganizationsByRegion(string region)
        {
            try
            {
                List<OrganizationDto> organizations = _organizationService.GetOrganizationsByRegion(region);
                
                if (organizations == null)
                    return NotFound("Region not found");

                return Ok(organizations);
            }
            catch (NotImplementedException ex)
            {
                // Handle the case where the method is not implemented
                return StatusCode(501, "Not Implemented: " + ex.Message);
            }
            catch (Exception ex)
            {
                // Catch other general exceptions
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }
    }
}
