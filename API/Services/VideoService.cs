using API.Entities;
using API.Entities.ViewModels;
using API.Infra.Interfaces;
using API.Services.Interfaces;
using AutoMapper;

namespace API.Services
{
    public class VideoService
    {
        private readonly IMapper _mapper;
        private readonly IMongoRepository<Video> _video;
        private readonly ICacheService _cacheService;
        private readonly string keyForCache = "video";

        public VideoService(IMapper mapper, IMongoRepository<Video> video, ICacheService cacheService)
        {
            _mapper = mapper;
            _video = video;
            _cacheService = cacheService;
        }

        public Result<VideoViewModel> Get(int page, int quantity)
        {
            var cacheKey = $"{keyForCache}/{page}/{quantity}";
            var video = _cacheService.Get<Result<VideoViewModel>>(cacheKey);

            if (video is null)
            {
                video = _mapper.Map<Result<VideoViewModel>>(_video.Get(page, quantity));
                _cacheService.Set(cacheKey, video);
            }

            return video;
        }


        public VideoViewModel Get(string id)
        {
            var cacheKey = $"{keyForCache}/{id}";
            var video = _cacheService.Get<VideoViewModel>(cacheKey);

            if (video is null)
            {
                video = _mapper.Map<VideoViewModel>(_video.Get(id));
                _cacheService.Set(cacheKey, video);
            }

            return video;
        }
            

        public VideoViewModel GetBySlug(string slug)
        {
            var cacheKey = $"{keyForCache}/{slug}";

            var video = _cacheService.Get<VideoViewModel>(cacheKey);

            if (video is null)
            {
                video = _mapper.Map<VideoViewModel>(_video.GetBySlug(slug));
                _cacheService.Set(cacheKey, video);
            }

            return video;
        }

        public VideoViewModel Create(VideoViewModel video)
        {
            var entity = new Video(video.Hat, video.Title, video.Author, video.Thumbnail, video.UrlVideo, video.Status);
            _video.Create(entity);

            var cacheKey = $"{keyForCache}/{entity.Slug}";
            _cacheService.Set(cacheKey, entity);

            return Get(entity.Id);
        }

        public void Update(string id, VideoViewModel videoIn)
        {
            var cacheKey = $"{keyForCache}/{id}";

            _video.Update(id, _mapper.Map<Video>(videoIn));

            _cacheService.Remove(cacheKey);
            _cacheService.Set(cacheKey, videoIn);
        }

        public void Remove(string id)
        {
            var video = Get(id);

            var cacheKey = $"{keyForCache}/{id}";
            _cacheService.Remove(cacheKey);

            cacheKey = $"{keyForCache}/{video.Slug}";
            _cacheService.Remove(cacheKey);

            _video.Remove(id);
        }
    }
}
