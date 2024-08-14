using BaseInsightDotNet.Business.InterfaceServices;
using BaseInsightDotNet.Commons.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BaseInsightDotNet.Presentation.Controllers
{
    [Route(Constant.DefaultValue.DEFAULT_CONTROLLER_ROUTE)]
    [ApiController]
    public class PositionController : ControllerBase
    {
        private readonly IPositionService _positionService;
        public PositionController(IPositionService positionService)
        {
            _positionService = positionService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPositions()
        {
            var result = await _positionService.GetAllPositions();
            return Ok(result);
        }
    }
}
