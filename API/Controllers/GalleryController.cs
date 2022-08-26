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
    public class GalleryController : ControllerBase
    {
        private readonly ILogger<GalleryController> _logger;
        private readonly GalleryService _galleryService;

        public GalleryController(ILogger<GalleryController> logger, GalleryService galleryService)
        {
            _logger = logger;
            _galleryService = galleryService;
        }

        [HttpGet("{page}/{quantity}")]
        public ActionResult<Result<GalleryViewModel>> Get(int page, int quantity) => _galleryService.Get(page, quantity);

        [HttpGet("{id}", Name = "GetGalleries")]
        public ActionResult<GalleryViewModel> Get(string id)
        {
            var gallery = _galleryService.Get(id);

            if (gallery is null) return NotFound();

            return gallery;
        }

        [HttpPost]
        public ActionResult<GalleryViewModel> Create(GalleryViewModel gallery)
        {
            var result = _galleryService.Create(gallery);

            return CreatedAtRoute("GetGalleries", new { id = result.Id.ToString() }, result);
        }

        [HttpPut("{id}")]
        public ActionResult<GalleryViewModel> Update(string id, GalleryViewModel galleryIn)
        {
            var gallery = _galleryService.Get(id);

            if (gallery is null) return NotFound();

            _galleryService.Update(id, galleryIn);

            return CreatedAtRoute("GetGalleries", new { id = id }, galleryIn);
        }

        [HttpDelete("{id}")]
        public ActionResult<GalleryViewModel> Delete(string id)
        {
            var gallery = _galleryService.Get(id);

            if (gallery is null) return NotFound();

            _galleryService.Remove(gallery.Id);

            var result = new
            {
                message = "Gallery deleted successfully!"
            };

            return Ok(result);
        }
    }
}