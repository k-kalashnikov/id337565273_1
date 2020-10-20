using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DomainModel = SP.Contract.Domains.AggregatesModel.Contract.Entities;

namespace SP.Contract.Persistence.Configurations
{
    internal class ContractTypeConfiguration : IEntityTypeConfiguration<DomainModel.ContractType>
    {
        public void Configure(EntityTypeBuilder<DomainModel.ContractType> builder)
        {
            builder.ToTable("ContractType");

            builder.HasKey(e => e.Id)
                .HasName("PK_ContractTypeID");

            builder.Property(e => e.Id).HasColumnName("ContractTypeID");

            builder.Property(o => o.Id)
                .ValueGeneratedNever()
                .IsRequired();

            builder.Property(o => o.Name)
                .HasMaxLength(255)
                .IsRequired();

            builder.HasData(DomainModel.ContractType.List());
        }
    }
}
