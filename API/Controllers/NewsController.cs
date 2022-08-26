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
    public class NewsController : ControllerBase
    {
        private readonly ILogger<NewsController> _logger;
        private readonly NewsService _newsService;

        public NewsController(ILogger<NewsController> logger, NewsService newsService)
        {
            _logger = logger;
            _newsService = newsService;
        }

        [HttpGet("{page}/{quantity}")]
        public ActionResult<Result<NewsViewModel>> Get(int page, int quantity) => _newsService.Get(page, quantity);

        [HttpGet("{id}", Name = "GetNews")]
        public ActionResult<NewsViewModel> Get(string id)
        {
            var news = _newsService.Get(id);

            if (news is null) return NotFound();

            return news;
        }

        [HttpPost]
        public ActionResult<NewsViewModel> Create(NewsViewModel news)
        {
            var result = _newsService.Create(news);

            return CreatedAtRoute("GetNews", new { id = result.Id.ToString() }, result);
        }

        [HttpPut("{id}")]
        public ActionResult<NewsViewModel> Update(string id, NewsViewModel newsIn)
        {
            var news = _newsService.Get(id);

            if (news is null) return NotFound();

            _newsService.Update(id, newsIn);

            return CreatedAtRoute("GetNews", new { id = id }, newsIn);
        }

        [HttpDelete("{id}")]
        public ActionResult<NewsViewModel> Delete(string id)
        {
            var news = _newsService.Get(id);

            if (news is null) return NotFound();

            _newsService.Remove(news.Id);

            var result = new
            {
                message = "News deleted successfully!"
            };

            return Ok(result);
        }
    }
}