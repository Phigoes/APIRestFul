using API.Entities;
using API.Entities.ViewModels;
using AutoMapper;

namespace API.Mappers
{
    public class EntityToViewModelMapping : Profile
    {
        public EntityToViewModelMapping()
        {
            #region [Entities]
            CreateMap<News, NewsViewModel>();
            CreateMap<Video, VideoViewModel>();
            CreateMap<Gallery, GalleryViewModel>();
            #endregion

            #region [Result<T>]
            CreateMap<Result<Video>, Result<VideoViewModel>>();
            CreateMap<Result<News>, Result<NewsViewModel>>();
            CreateMap<Result<Gallery>, Result<GalleryViewModel>>();
            #endregion
        }
    }
}
