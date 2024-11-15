using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ClassManagement.Api.Data.Entities;

namespace ClassManagement.Api.Data.Configurations
{
    public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> builder)
        {
            builder.ToTable("Subjects");

            builder.Property(x => x.Id).HasMaxLength(20);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(255);

            builder.Property(x => x.Status).IsRequired().HasMaxLength(255);

            builder.Property(x => x.Credit).IsRequired();
        }
    }
}
