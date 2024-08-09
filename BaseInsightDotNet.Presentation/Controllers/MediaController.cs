using BaseInsightDotNet.Business.InterfaceServices;
using BaseInsightDotNet.Commons.Constants;
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
        public async Task<ActionResult> UploadPhoto()
        {
            string folderName = "MediaFiles\\Admissions\\FilesUpload\\Photos";
            string webRootPath = _hostingEnvironment.WebRootPath;
            string newPath = Path.Combine(webRootPath, folderName);
            if (!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
            }
            try
            {
                var results = new List<UploadPhotoUseCaseOutput>();
                foreach (var file in Request.Form.Files)
                {
                    using (var stream = file.OpenReadStream())
                    {
                        var useCase = _serviceProvider
                            .GetService<IUseCase<UploadPhotoUseCaseInput, UploadPhotoUseCaseOutput>>()
                            .NotNull();
                        var uploadPhotoUseCaseInput = new UploadPhotoUseCaseInput
                        {
                            FileName = file.FileName,
                            FileStream = stream,
                            SavePath = newPath
                        };
                        var mediaFile = await useCase.ExecuteAsync(uploadPhotoUseCaseInput);
                        results.Add(mediaFile);
                    }
                }
                return Ok(results);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("admissions/{fileId}/download")]
        public async Task<IActionResult> DownloadAsync(Guid fileId)
        {
            var useCase = _serviceProvider
                .GetService<IUseCase<DownloadFileUseCaseInput, DownloadFileUseCaseOutput>>()
                .NotNull();
            var useCaseInput = new DownloadFileUseCaseInput
            {
                Id = fileId
            };
            var mediaFile = await useCase.ExecuteAsync(useCaseInput);
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
        [Authorize(Policy = "ApiScope")]
        [HttpGet]
        [Route("admissions/test")]
        public async Task<IActionResult> Test()
        {
            return Ok("ok");
        }
    }
}
