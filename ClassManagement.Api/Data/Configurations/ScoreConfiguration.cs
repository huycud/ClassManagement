using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ClassManagement.Api.Data.Entities;

namespace ClassManagement.Api.Data.Configurations
{
    public class ScoreConfiguration : IEntityTypeConfiguration<Score>
    {
        public void Configure(EntityTypeBuilder<Score> builder)
        {
            builder.ToTable("Scores");

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Process).IsRequired();

            builder.Property(x => x.MidTerm).IsRequired();

            builder.Property(x => x.FinalTest).IsRequired();

            builder.Property(x => x.Average).IsRequired();
        }
    }
}
