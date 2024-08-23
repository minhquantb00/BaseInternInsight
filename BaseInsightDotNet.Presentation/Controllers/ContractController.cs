using BaseInsightDotNet.Business.ImplementServices;
using BaseInsightDotNet.Business.InterfaceServices;
using BaseInsightDotNet.Business.Payloads.RequestModels.ContractRequest;
using BaseInsightDotNet.Business.Payloads.RequestModels.FilterRequest;
using BaseInsightDotNet.Business.Payloads.RequestModels.MediaRequest;
using BaseInsightDotNet.Business.Payloads.ResponseModels.DataMedia;
using BaseInsightDotNet.Commons.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BaseInsightDotNet.Presentation.Controllers
{
    [Route(Constant.DefaultValue.DEFAULT_CONTROLLER_ROUTE)]
    [ApiController]
    public class ContractController : ControllerBase
    {
        private readonly IContractService _contractService;
        private readonly IMediaService _mediaService;
        public ContractController(IContractService contractService, IMediaService mediaService)
        {
            _contractService = contractService;
            _mediaService = mediaService;
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> CreateContract([FromBody] Request_CreateContract request)
        {
            return Ok(await _contractService.CreateContract(request));
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UpdateContract([FromBody] Request_UpdateContract request)
        {
            return Ok(await _contractService.UpdateContract(request));
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeleteContract([FromRoute] Guid id)
        {
            return Ok(await _contractService.DeleteContract(id));
        }

        [HttpGet]
        [Consumes(contentType: "multipart/form-data")]
        public async Task<IActionResult> GetAllContracts([FromForm] Request_FilterContract request)
        {
            return Ok(await _contractService.GetAllContracts(request));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetContractById([FromRoute] Guid id)
        {
            return Ok(await _contractService.GetContractById(id));
        }

        [HttpPost("{id}")]
        [Consumes(contentType: "multipart/form-data")]
        public async Task<IActionResult> UploadPhotoContract([FromRoute] Guid id , [FromForm] IEnumerable<IFormFile> files)
        {
            string relativePath = Path.Combine(Directory.GetCurrentDirectory(), "MediaFiles", "Admissions", "FilesUpload", "Photos");

            if (!Directory.Exists(relativePath))
            {
                Directory.CreateDirectory(relativePath);
            }

            try
            {
                var results = new List<DataResponseUploadPhoto>();
                foreach (var file in files)
                {
                    try
                    {
                        if (file.Length <= 0)
                        {
                            return BadRequest("Empty file uploaded.");
                        }

                        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".pdf", ".docx", ".exe", ".mp4", ".ppt", ".ppxs", ".potx", ".msi", ".html" };
                        var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();

                        if (!allowedExtensions.Contains(fileExtension))
                        {
                            return BadRequest("Invalid file type.");
                        }

                        using (var stream = file.OpenReadStream())
                        {
                            List<Request_UploadPhoto> list = new List<Request_UploadPhoto>();
                            Request_UploadPhoto uploadFileRequest = new Request_UploadPhoto
                            {
                                FileName = file.FileName,
                                FileStream = stream,
                                SavePath = relativePath
                            };
                            list.Add(uploadFileRequest);
                            var request = new Request_UploadPhotoContract
                            {
                                Files = list,
                                ContractId = id
                            };
                            await _contractService.UploadPhotoContract(request);
                            //results.Add(mediaFile);
                        }
                    }
                    catch (Exception ex)
                    {
                        return BadRequest($"Error uploading file {file.FileName}: {ex.Message}");
                    }
                }
                return Ok(results);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }
    }
}
