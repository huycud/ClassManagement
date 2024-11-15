using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ClassManagement.Api.Data.Entities;

namespace ClassManagement.Api.Data.Configurations
{
    public class HomeworkConfiguration : IEntityTypeConfiguration<Homework>
    {
        public void Configure(EntityTypeBuilder<Homework> builder)
        {
            builder.ToTable("Homeworks");

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasOne(t => t.Class).WithMany(pc => pc.Homeworks).HasForeignKey(t => t.ClassId).OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.Title).IsRequired();

            builder.Property(x => x.Description).IsRequired();
        }
    }
}
