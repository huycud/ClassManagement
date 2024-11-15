using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ClassManagement.Api.Data.Entities;

namespace ClassManagement.Api.Data.Configurations
{
    public class ClassConfiguration : IEntityTypeConfiguration<Class>
    {
        public void Configure(EntityTypeBuilder<Class> builder)
        {
            builder.ToTable("Classes");

            builder.Property(x => x.Id).HasMaxLength(20);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(255);

            builder.Property(x => x.ClassSize).IsRequired();

            builder.Property(x => x.Amount).IsRequired();

            builder.Property(x => x.Credit).IsRequired();

            builder.Property(x => x.ClassPeriods).IsRequired();

            builder.Property(x => x.DayOfWeek).IsRequired();

            //Class chỉ được xóa khi không có user nào có liên kết đến class từ user thông qua classId(class rỗng)
            builder.HasOne(t => t.User).WithMany(pc => pc.Classes).HasForeignKey(pc => pc.UserId).OnDelete(DeleteBehavior.Restrict);

            //Semester chỉ được xóa khi không có liên kết nào đến Semester từ class thông qua SemesterId
            builder.HasOne(x => x.Semester).WithMany(x => x.Classes).HasForeignKey(x => x.SemesterId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
