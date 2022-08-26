using API.Entities;
using API.Entities.ViewModels;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GalleryExternalController : ControllerBase
    {
        private readonly ILogger<GalleryExternalController> _logger;
        private readonly GalleryService _galleryService;

        public GalleryExternalController(ILogger<GalleryExternalController> logger, GalleryService galleryService)
        {
            _logger = logger;
            _galleryService = galleryService;
        }

        [HttpGet("{page}/{quantity}")]
        public ActionResult<Result<GalleryViewModel>> Get(int page, int quantity) => 
            _galleryService.Get(page, quantity);

        [HttpGet("{slug}")]
        public ActionResult<GalleryViewModel> Get(string slug)
        {
            var gallery = _galleryService.GetBySlug(slug);

            if (gallery is null) return NotFound();

            return gallery;
        }
    }
}