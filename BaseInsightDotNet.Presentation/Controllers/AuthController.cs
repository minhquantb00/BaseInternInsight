using BaseInsightDotNet.Business.InterfaceServices;
using BaseInsightDotNet.Business.Payloads.RequestModels.UserRequest;
using BaseInsightDotNet.Commons.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BaseInsightDotNet.Presentation.Controllers
{
    [Route(Constant.DefaultValue.DEFAULT_CONTROLLER_ROUTE)]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost]
        [Consumes(contentType: "multipart/form-data")]
        public async Task<IActionResult> RegisterUser([FromForm] Request_Register request)
        {
            return Ok(await _authService.RegisterUser(request));
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] Request_Login request)
        {
            return Ok(await _authService.Login(request));
        }
    }
} 
