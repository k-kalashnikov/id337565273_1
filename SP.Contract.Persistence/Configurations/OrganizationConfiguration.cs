using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SP.Contract.Domains.AggregatesModel.Misc.Entities;

namespace SP.Contract.Persistence.Configurations
{
    internal class OrganizationConfiguration : IEntityTypeConfiguration<Organization>
    {
        public void Configure(EntityTypeBuilder<Organization> builder)
        {
            builder.ToTable("Organization");

            builder.HasKey(e => e.Id)
                .HasName("PK_OrganizationID");

            builder.Property(e => e.Id)
                .HasColumnName("OrganizationID")
                .ValueGeneratedNever();

            builder.Property(o => o.Name)
                .HasMaxLength(255)
                .IsRequired();
        }
    }
}
