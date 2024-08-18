using BaseInsightDotNet.Business.InterfaceServices;
using BaseInsightDotNet.Business.Payloads.RequestModels.ContractRequest;
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
    public class ContractTypeController : ControllerBase
    {
        private readonly IContractTypeService _contractTypeService;
        public ContractTypeController(IContractTypeService contractTypeService)
        {
            _contractTypeService = contractTypeService;
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> CreateContractType([FromBody] Request_CreateContractType request)
        {
            var result = await _contractTypeService.CreateContractType(request);
            return Ok(result);
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UpdateContractType([FromBody] Request_UpdateContractType request)
        {
            var result = await _contractTypeService.UpdateContractType(request);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeleteContractType([FromRoute] Guid id)
        {
            var result = await _contractTypeService.DeleteContractType(id);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllContractTypes([FromQuery] Request_FilterContractType request)
        {
            var result = await _contractTypeService.GetAllContractTypes(request);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetContractTypeById([FromRoute] Guid id)
        {
            var result = await _contractTypeService.GetContractTypeById(id);
            return Ok(result);
        }
    }
}
