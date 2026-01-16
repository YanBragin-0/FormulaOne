using FormulaOne.Application.Services.ElasticSearch;
using FormulaOne.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FormulaOne.Controllers.ElasticControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = nameof(Roles.Admin))]
    public class AdminElasticController(AdminSearchService searchService) : ControllerBase
    {
        private readonly AdminSearchService _searchService = searchService;
        [HttpPost("bulkToElastic")]
        public async Task<IActionResult> BulkToElastic()
        {
            await _searchService.ElasticReindex();
            return Ok();
        }
    }
}
