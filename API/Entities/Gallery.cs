using API.Entities.Enums;
using API.Infra;

namespace API.Entities
{
    public class Gallery : BaseEntity
    {
        public Gallery(string title, string legend, string author, string tags, string thumb, Status status, IList<string> galleryImages)
        {
            Title = title;
            Legend = legend;
            Author = author;
            Tags = tags;
            Thumb = thumb;
            Slug = Helper.GenerateSlug(Title);
            PublishDate = DateTime.Now;
            Status = status;
            GalleryImages = galleryImages;

            ValidateEntity();
        }

        public string Title { get; private set; }
        public string Legend { get; private set; }
        public string Author { get; private set; }
        public string Tags { get; private set; }
        public string Thumb { get; private set; }
        public IList<string> GalleryImages { get; set; }

        public void ValidateEntity()
        {
            AssertionConcern.AssertArgumentNotEmpty(Title, "Title must not be empty");
            AssertionConcern.AssertArgumentNotEmpty(Legend, "Legend must not be empty");

            AssertionConcern.AssertArgumentLength(Title, 90, "Title must have until 90 characters");
            AssertionConcern.AssertArgumentLength(Legend, 40, "Legend must have until 40 characters");
        }
    }
}
