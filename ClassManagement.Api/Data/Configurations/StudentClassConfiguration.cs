using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ClassManagement.Api.Data.Entities;

namespace ClassManagement.Api.Data.Configurations
{
    public class StudentClassConfiguration : IEntityTypeConfiguration<StudentClass>
    {
        public void Configure(EntityTypeBuilder<StudentClass> builder)
        {
            builder.ToTable("StudentsClasses").HasKey(x => new { x.UserId, x.ClassId });

            builder.HasOne(t => t.User).WithMany(pc => pc.StudentClasses).HasForeignKey(pc => pc.UserId).OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(t => t.Class).WithMany(pc => pc.StudentClasses).HasForeignKey(pc => pc.ClassId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
