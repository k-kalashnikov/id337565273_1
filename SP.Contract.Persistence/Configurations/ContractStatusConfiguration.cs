using System.Xml.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DomainModel = SP.Contract.Domains.AggregatesModel.Contract.Entities;

namespace SP.Contract.Persistence.Configurations
{
    internal class ContractStatusConfiguration : IEntityTypeConfiguration<DomainModel.ContractStatus>
    {
        public void Configure(EntityTypeBuilder<DomainModel.ContractStatus> builder)
        {
            builder.ToTable("ContractStatus");

            builder.HasKey(e => e.Id)
                .HasName("PK_ContractStatusID");

            builder.Property(e => e.Id).HasColumnName("ContractStatusID");

            builder.Property(o => o.Id)
                .ValueGeneratedNever()
                .IsRequired();

            builder.Property(o => o.Name)
                .HasMaxLength(255)
                .IsRequired();

            builder.HasData(DomainModel.ContractStatus.List());
        }
    }
}
