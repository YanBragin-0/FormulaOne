using FormulaOne.Application.Services.Abstractions.ElasticInterfaces;
using FormulaOne.Application.Services.ElasticSearch;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FormulaOne.Controllers.ElasticControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class SearchController(ISearchService search) : ControllerBase
    {
        private readonly ISearchService _search = search;
        [HttpGet("Search")]
        public async Task<IActionResult> Search([FromQuery] string queryString)
        {
            if (string.IsNullOrWhiteSpace(queryString))
            {
                return NotFound("По данному запросу ничего не найдено");
            }
            var result = await _search.SearchAsync(queryString);
            return result != null ? Ok(result) : BadRequest("Произошла ошибка поиска");
        }
        
    }
}
