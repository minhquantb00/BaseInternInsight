using BaseInsightDotNet.Business.InterfaceServices;
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
    public class MediaController : ControllerBase
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IMediaService _mediaService;
        public MediaController(IWebHostEnvironment hostingEnvironment, IMediaService mediaService)
        {
            _hostingEnvironment = hostingEnvironment;
            _mediaService = mediaService;
        }

        [DisableRequestSizeLimit]
        [HttpPost("admissions/uploadphoto")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> UploadPhoto([FromForm] IEnumerable<IFormFile> files)
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

                        // Kiểm tra loại file được phép
                        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".pdf", ".docx", ".exe", ".mp4", ".ppt", ".ppxs",  ".potx", ".msi", ".html" };
                        var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();

                        if (!allowedExtensions.Contains(fileExtension))
                        {
                            return BadRequest("Invalid file type.");
                        }

                        using (var stream = file.OpenReadStream())
                        {
                            var request = new Request_UploadPhoto
                            {
                                FileName = file.FileName,
                                FileStream = stream,
                                SavePath = relativePath
                            };
                            var mediaFile = await _mediaService.HandleUploadPhoto(request);
                            results.Add(mediaFile);
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





        [HttpGet]
        [Route("admissions/{fileId}/download")]
        public async Task<IActionResult> DownloadAsync(Guid fileId)
        {
            var useCaseInput = new Request_DownloadFile
            {
                Id = fileId
            };
            var mediaFile = await _mediaService.HandleDownloadFile(useCaseInput);
            if (mediaFile == null || !System.IO.File.Exists(mediaFile.Path))
                return BadRequest("File not found!");

            var memory = new MemoryStream();
            using (var stream = new FileStream(mediaFile.Path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }

            memory.Position = 0;
            return File(memory, mediaFile.MimeType, mediaFile.Name);
        }
        [HttpGet]
        [Route("admissions/test")]
        public async Task<IActionResult> Test()
        {
            return Ok("ok");
        }
    }
}
