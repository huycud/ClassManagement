using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ClassManagement.Api.Data.Entities;

namespace ClassManagement.Api.Data.Configurations
{
    public class NotifyConfiguration : IEntityTypeConfiguration<Notify>
    {
        public void Configure(EntityTypeBuilder<Notify> builder)
        {
            builder.ToTable("Notifies");

            builder.Property(x => x.Id).HasMaxLength(255);

            builder.HasOne(t => t.User).WithMany(pc => pc.Notifies).HasForeignKey(t => t.UserId).OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.Title).IsRequired().HasMaxLength(255);

            builder.Property(x => x.Content).IsRequired();

            builder.Property(x => x.Type).IsRequired();
        }
    }
}
