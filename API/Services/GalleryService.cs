using API.Entities;
using API.Entities.ViewModels;
using API.Infra.Interfaces;
using API.Services.Interfaces;
using AutoMapper;

namespace API.Services
{
    public class GalleryService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Gallery> _gallery;
        private readonly ICacheService _cacheService;
        private readonly string keyForCache = "gallery";

        public GalleryService(IRepository<Gallery> gallery, IMapper mapper, ICacheService cacheService)
        {
            _gallery = gallery;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        public Result<GalleryViewModel> Get(int page, int quantity)
        {
            var cacheKey = $"{keyForCache}/{page}/{quantity}";
            var gallery = _cacheService.Get<Result<GalleryViewModel>>(cacheKey);

            if (gallery is null)
            {
                gallery = _mapper.Map<Result<GalleryViewModel>>(_gallery.Get(page, quantity));
                _cacheService.Set(cacheKey, gallery);
            }

            return gallery;
        }

        public GalleryViewModel Get(string id)
        {
            var cacheKey = $"{keyForCache}/{id}";

            var gallery = _cacheService.Get<GalleryViewModel>(cacheKey);

            if (gallery is null)
            {
                gallery = _mapper.Map<GalleryViewModel>(_gallery.Get(id));
                _cacheService.Set(cacheKey, gallery);
            }

            return gallery;
        }

            

        public GalleryViewModel GetBySlug(string slug)
        {
            var cacheKey = $"{keyForCache}/{slug}";

            var gallery = _cacheService.Get<GalleryViewModel>(cacheKey);

            if (gallery is null)
            {
                gallery = _mapper.Map<GalleryViewModel>(_gallery.GetBySlug(slug));
                _cacheService.Set(cacheKey, gallery);
            }

            return gallery;
        }

        public GalleryViewModel Create(GalleryViewModel gallery)
        {
            var entity = new Gallery(gallery.Title, gallery.Legend, gallery.Author, gallery.Tags, gallery.Thumb, gallery.Status, gallery.GalleryImages);
            _gallery.Create(entity);

            var cacheKey = $"{keyForCache}/{entity.Slug}";
            _cacheService.Set(cacheKey, entity);

            return Get(entity.Id);
        }

        public void Update(string id, GalleryViewModel galleryIn)
        {
            var cacheKey = $"{keyForCache}/{id}";

            _gallery.Update(id, _mapper.Map<Gallery>(galleryIn));

            _cacheService.Remove(cacheKey);
            _cacheService.Set(cacheKey, galleryIn);
        }

        public void Remove(string id)
        {
            var gallery = Get(id);

            var cacheKey = $"{keyForCache}/{id}";
            _cacheService.Remove(cacheKey);

            cacheKey = $"{keyForCache}/{gallery.Slug}";
            _cacheService.Remove(cacheKey);

            _gallery.Remove(id);
        }
    }
}
