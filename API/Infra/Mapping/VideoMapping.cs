using API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Infra.Mapping
{
    public class VideoMapping : IEntityTypeConfiguration<Video>
    {
        public void Configure(EntityTypeBuilder<Video> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Title)
                .HasColumnType("varchar(250)");

            builder.Property(c => c.Hat)
                .HasColumnType("varchar(80)");

            builder.Property(c => c.Author)
                .HasColumnType("varchar(80)");

            builder.Property(c => c.Thumbnail)
                .HasColumnType("varchar(255)");

            builder.Property(c => c.UrlVideo)
                .HasColumnType("varchar(255)");

            builder.Property(c => c.Slug)
                .HasColumnType("varchar(255)");

            builder.Property(c => c.Status);

            builder.Property(c => c.PublishDate)
                .HasColumnType("varchar(255)");

            builder.Property(c => c.Deleted)
                .HasColumnType("bit");

            builder.ToTable("Video");
        }
    }
}
