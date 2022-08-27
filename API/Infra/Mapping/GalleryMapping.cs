using API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using YamlDotNet.Core.Tokens;

namespace API.Infra.Mapping
{
    public class GalleryMapping : IEntityTypeConfiguration<Gallery>
    {
        public string Title { get; private set; }
        public string Legend { get; private set; }
        public string Author { get; private set; }
        public string Tags { get; private set; }
        public string Thumb { get; private set; }
        public IList<string> GalleryImages { get; set; }

        public void Configure(EntityTypeBuilder<Gallery> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Title)
                .HasColumnType("varchar(250)");

            builder.Property(c => c.Legend)
                .HasColumnType("varchar(80)");

            builder.Property(c => c.Author)
                .HasColumnType("varchar(80)");

            builder.Property(c => c.Tags)
                .HasColumnType("varchar(80)");

            builder.Property(c => c.Thumb)
                .HasColumnType("varchar(255)");

            builder.Property(c => c.GalleryImages)
                .HasColumnType("varchar(255)")
                .HasConversion(
                    value => JsonConvert.SerializeObject(value),
                    value => JsonConvert.DeserializeObject<IList<string>>(value));

            builder.Property(c => c.Slug)
                .HasColumnType("varchar(255)");

            builder.Property(c => c.Status);

            builder.Property(c => c.PublishDate);

            builder.Property(c => c.Deleted)
                .HasColumnType("int");

            builder.ToTable("Gallery");
        }
    }
}
