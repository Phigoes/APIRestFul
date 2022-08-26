using API.Entities;
using API.Entities.ViewModels;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class VideoController : ControllerBase
    {
        private readonly ILogger<VideoController> _logger;
        private readonly VideoService _videoService;

        public VideoController(ILogger<VideoController> logger, VideoService videoService)
        {
            _logger = logger;
            _videoService = videoService;
        }

        [HttpGet("{page}/{quantity}")]
        public ActionResult<Result<VideoViewModel>> Get(int page, int quantity) => 
            _videoService.Get(page, quantity);

        [HttpGet("{id:length(24)}", Name = "GetVideos")]
        public ActionResult<VideoViewModel> Get(string id)
        {
            var video = _videoService.Get(id);

            if (video is null) return NotFound();

            return video;
        }

        [HttpPost]
        public ActionResult<VideoViewModel> Create(VideoViewModel video)
        {
            var result = _videoService.Create(video);

            return CreatedAtRoute("GetVideos", new { id = result.Id.ToString() }, result);
        }

        [HttpPut("{id:length(24)}")]
        public ActionResult<VideoViewModel> Update(string id, VideoViewModel videoIn)
        {
            var video = _videoService.Get(id);

            if (video is null) return NotFound();

            _videoService.Update(id, videoIn);

            return CreatedAtRoute("GetVideos", new { id = id }, videoIn);
        }

        [HttpDelete("{id:length(24)}")]
        public ActionResult<VideoViewModel> Delete(string id)
        {
            var video = _videoService.Get(id);

            if (video is null) return NotFound();

            _videoService.Remove(id);

            var result = new
            {
                message = "Video deleted successfully!"
            };

            return Ok(result);
        }
    }
}