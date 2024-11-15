using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ClassManagement.Api.Data.Entities;

namespace ClassManagement.Api.Data.Configurations
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("Clients");

            builder.Property(x => x.Firstname).IsRequired().HasMaxLength(255);

            builder.Property(x => x.Lastname).IsRequired().HasMaxLength(255);

            builder.Property(x => x.Address).IsRequired().HasMaxLength(255);

            builder.Property(x => x.DateOfBirth).IsRequired();

            builder.Property(x => x.Gender).IsRequired();
        }
    }
}
