using API.Entities.Enums;
using API.Infra;

namespace API.Entities
{
    public class Video : BaseEntity
    {
        public Video(string hat, string title, string author, string thumbnail, string urlVideo, Status status)
        {
            Hat = hat;
            Title = title;
            Author = author;
            Thumbnail = thumbnail;
            PublishDate = DateTime.Now;
            Slug = Helper.GenerateSlug(Title);
            UrlVideo = urlVideo;
            Status = status;

            ValidateEntity();
        }

        public string Hat { get; private set; }
        public string Title { get; private set; }
        public string Author { get; private set; }
        public string Thumbnail { get; private set; }
        public string UrlVideo { get; private set; }

        public void ValidateEntity()
        {
            AssertionConcern.AssertArgumentNotEmpty(Title, "Title must not be empty");
            AssertionConcern.AssertArgumentNotEmpty(Hat, "Hat must not be empty");

            AssertionConcern.AssertArgumentLength(Title, 90, "Title must have until 90 characters");
            AssertionConcern.AssertArgumentLength(Hat, 40, "Hat must have until 40 characters");
        }
    }
}
