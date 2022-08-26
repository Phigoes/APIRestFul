using API.Entities;
using API.Entities.Enums;
using ImageProcessor;
using ImageProcessor.Plugins.WebP.Imaging.Formats;

namespace API.Services
{
    public class UploadService
    {
        public string UploadFile(IFormFile file)
        {
            var validateTypeMedia = GetTypeMedia(file.FileName);
            return validateTypeMedia == Media.Image ? UploadImage(file) : UploadVideo(file);
        }

        public Media GetTypeMedia(string fileName)
        {
            string[] imageExtensions = { ".png", ".jpg", ".jpeg", ".webp" };

            string[] videoExtensions = { ".avi", ".mp4" };

            var fileInfo = new FileInfo(fileName);

            return imageExtensions.Contains(fileInfo.Extension) ? 
                Media.Image : videoExtensions.Contains(fileInfo.Extension) ? 
                Media.Video : throw new DomainException("File format invalid.");
        }

        private string UploadImage(IFormFile file)
        {
            using (var stream = new FileStream(Path.Combine("Medias/Images", file.FileName), FileMode.Create))
            {
                file.CopyTo(stream);
            }

            var urlFile = Guid.NewGuid() + ".webp";

            using (var webPFileStream = new FileStream(Path.Combine("Medias/Images", urlFile), FileMode.Create))
            {
                using (ImageFactory imageFactory = new ImageFactory(preserveExifData: false))
                {
                    imageFactory.Load(file.OpenReadStream())
                        .Format(new WebPFormat())
                        .Quality(100)
                        .Save(webPFileStream);
                }
            }

            return $"http://localhost:5036/medias/images/{urlFile}";
        }

        private string UploadVideo(IFormFile file)
        {
            FileInfo fileInfo = new FileInfo(file.FileName);

            var fileName = Guid.NewGuid() + fileInfo.Extension;

            using (var stream = new FileStream(Path.Combine("Medias/Videos", fileName), FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return $"http://localhost:5036/medias/videos/{fileName}";
        }

        private string UploadImageWithoutOriginalFile(IFormFile file)
        {
            var urlFile = Guid.NewGuid() + ".webp";

            using (MemoryStream stream = new MemoryStream())
            {
                file.CopyTo(stream);

                using (var webPFileStream = new FileStream(Path.Combine("Medias/Images", urlFile), FileMode.Create))
                {
                    using (ImageFactory imageFactory = new ImageFactory(preserveExifData: false))
                    {
                        imageFactory.Load(file.OpenReadStream())
                            .Format(new WebPFormat())
                            .Quality(100)
                            .Save(webPFileStream);
                    }
                }
            }

            return $"http://localhost:5036/medias/images/{urlFile}";
        }
    }
}