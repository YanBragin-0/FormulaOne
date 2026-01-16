using FormulaOne.Application.Services.Abstractions.ReadInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FormulaOne.Controllers.ReadControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class ConstructorsChampionshipReadController(IReadConstrCShService service) 
         : ControllerBase
    {
        private readonly IReadConstrCShService _readConstrCShService = service;
        [HttpGet("GetComstructorsTable")]
        public async Task<IActionResult> GetTableByYear([FromQuery] short Year)
        {
            var result = await _readConstrCShService.GetTeableBySeason(Year);
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.ErrorMessage);
        }
    }
}
