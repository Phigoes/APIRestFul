using API.Entities.Enums;
using API.Infra;
using MongoDB.Bson.Serialization.Attributes;

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

        [BsonElement("title")]
        public string Title { get; private set; }

        [BsonElement("legend")]
        public string Legend { get; private set; }

        [BsonElement("author")]
        public string Author { get; private set; }

        [BsonElement("tags")]
        public string Tags { get; private set; }

        [BsonElement("thumb")]
        public string Thumb { get; private set; }

        [BsonElement("galleryImages")]
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
