using BaseInsightDotNet.Business.InterfaceServices;
using BaseInsightDotNet.Business.Payloads;
using BaseInsightDotNet.Business.Payloads.RequestModels.DepartmentRequest;
using BaseInsightDotNet.Business.Payloads.RequestModels.FilterRequest;
using BaseInsightDotNet.Business.Payloads.ResponseModels.DataDepartment;
using BaseInsightDotNet.Commons.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BaseInsightDotNet.Presentation.Controllers
{
    [Route(Constant.DefaultValue.DEFAULT_CONTROLLER_ROUTE)]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllDepartments([FromQuery]Request_FilterDepartment? request)
        {
            var result = await _departmentService.GetAllDepartments(request);
            return Ok(result);
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> CreateDepartment([FromBody] Request_CreateDepartment request)
        {
            var result = await _departmentService.CreateDepartment(request);
            return HandleResponse(result);
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UpdateDepartment([FromBody] Request_UpdateDepartment request)
        {
            var result = await _departmentService.UpdateDepartment(request);
            return HandleResponse(result);
        }
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeleteDepartment([FromRoute] Guid id)
        {
            return Ok(await _departmentService.DeleteDepartment(id));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartmentById([FromRoute] Guid id)
        {
            return Ok(await _departmentService.GetDepartmentById(id));
        }

        #region private methods
        private IActionResult HandleResponse(ResponseObject<DataResponseDepartment> result)
        {
            switch (result.Status)
            {
                case 200:
                    return Ok(result);
                case 404:
                    return NotFound(result);
                case 400:
                    return BadRequest(result);
                case 401:
                    return Unauthorized(result);
                case 403:
                    return Forbid();
                case 500:
                    return StatusCode(StatusCodes.Status500InternalServerError, result);
                default:
                    return StatusCode(StatusCodes.Status500InternalServerError, result);
            }
        }
        #endregion
    }
}
