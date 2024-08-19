using BaseInsightDotNet.Business.InterfaceServices;
using BaseInsightDotNet.Business.Payloads.RequestModels.AllowanceRequest;
using BaseInsightDotNet.Business.Payloads.RequestModels.FilterRequest;
using BaseInsightDotNet.Commons.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BaseInsightDotNet.Presentation.Controllers
{
    [Route(Constant.DefaultValue.DEFAULT_CONTROLLER_ROUTE)]
    [ApiController]
    public class AllowanceController : ControllerBase
    {
        private readonly IAllowanceService _allowanceService;
        public AllowanceController(IAllowanceService allowanceService)
        {
            _allowanceService = allowanceService;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> CreateAllowance([FromBody] Request_CreateAllowance request)
        {
            return Ok(await _allowanceService.CreateAllowance(request));
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UpdateAllowance([FromBody] Request_UpdateAllowance request)
        {
            return Ok(await _allowanceService.UpdateAllowance(request));
        }
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeleteAllowance([FromRoute] Guid id)
        {
            return Ok(await _allowanceService.DeleteAllowance(id));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllowanceById([FromRoute] Guid id)
        {
            return Ok(await _allowanceService.GetAllowanceById(id));
        }
        [HttpGet]        
        
        public async Task<IActionResult> GetAllAllowances([FromQuery] Request_FilterAllowance request)
        {
            return Ok(await _allowanceService.GetAllAllowances(request));
        }
    }
}
