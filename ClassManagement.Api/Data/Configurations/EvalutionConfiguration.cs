using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ClassManagement.Api.Data.Entities;

namespace ClassManagement.Api.Data.Configurations
{
    public class EvalutionConfiguration : IEntityTypeConfiguration<Evalution>
    {
        public void Configure(EntityTypeBuilder<Evalution> builder)
        {
            builder.ToTable("Evalutions");

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.AcademicAbility).IsRequired().HasMaxLength(255);

            builder.Property(x => x.MoralTraining).IsRequired().HasMaxLength(255);
        }
    }
}
