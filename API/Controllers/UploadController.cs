using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly ILogger<UploadController> _logger;
        private readonly UploadService _uploadService;

        public UploadController(ILogger<UploadController> logger, UploadService uploadService)
        {
            _logger = logger;
            _uploadService = uploadService;
        }

        [HttpPost]
        public IActionResult Post(IFormFile file)
        {
            try
            {
                if (file == null) return null;

                var urlFile = _uploadService.UploadFile(file);

                return Ok(new
                {
                    message = "File saved successfully!",
                    urlImagem = urlFile
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Upload Error: " + ex.Message);
            }
        }
    }
}
