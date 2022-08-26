using API.Entities.Enums;

namespace API.Entities
{
    public abstract class BaseEntity
    {
        public string Id { get; set; }
        public bool Deleted { get; set; }
        public string Slug { get; set; }
        public DateTime PublishDate { get; protected set; }
        public Status Status { get; protected set; }
    }
}
