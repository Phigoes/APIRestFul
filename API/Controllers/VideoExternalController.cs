using API.Entities;
using API.Entities.ViewModels;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VideoExternalController : ControllerBase
    {
        private readonly ILogger<VideoExternalController> _logger;
        private readonly VideoService _videoService;

        public VideoExternalController(ILogger<VideoExternalController> logger, VideoService videoService)
        {
            _logger = logger;
            _videoService = videoService;
        }

        [HttpGet]
        public ActionResult<Result<VideoViewModel>> Get(int page, int quantity) =>
            _videoService.Get(page, quantity);

        [HttpGet("{slug}")]
        public ActionResult<VideoViewModel> Get(string slug)
        {
            var video = _videoService.GetBySlug(slug);

            if (video is null) return NotFound();

            return video;
        }
    }
}