using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ClassManagement.Api.Data.Entities;

namespace ClassManagement.Api.Data.Configurations
{
    public class ImageConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.ToTable("Images");

            builder.HasOne(t => t.User).WithOne(pc => pc.Images).HasForeignKey<Image>(t => t.UserId).OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.ImageName).HasMaxLength(255);

            builder.Property(x => x.ImagePath).IsRequired().HasMaxLength(255);

            builder.Property(x => x.Caption).HasMaxLength(255);
        }
    }
}
