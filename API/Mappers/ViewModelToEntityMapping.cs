using API.Entities;
using API.Entities.ViewModels;
using AutoMapper;

namespace API.Mappers
{
    public class ViewModelToEntityMapping : Profile
    {
        public ViewModelToEntityMapping()
        {
            #region [Entities]
            CreateMap<NewsViewModel, News>();
            CreateMap<VideoViewModel, Video>();
            CreateMap<GalleryViewModel, Gallery>();
            #endregion

            #region [Result<T>]
            CreateMap<Result<VideoViewModel>, Result<Video>>();
            CreateMap<Result<NewsViewModel>, Result<News>>();
            CreateMap<Result<GalleryViewModel>, Result<Gallery>>();
            #endregion
        }
    }
}
