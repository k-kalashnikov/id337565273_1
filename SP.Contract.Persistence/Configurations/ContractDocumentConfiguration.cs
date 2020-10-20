using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DomainModel = SP.Contract.Domains.AggregatesModel.Contract.Entities;

namespace SP.Contract.Persistence.Configurations
{
    public class ContractDocumentConfiguration : IEntityTypeConfiguration<DomainModel.ContractDocument>
    {
        public void Configure(EntityTypeBuilder<DomainModel.ContractDocument> builder)
        {
            builder.ToTable("ContractDocument");

            builder.HasKey(e => e.Id)
                .HasName("PK_ContractDocumentID");

            builder.Property(e => e.Id).HasColumnName("ContractDocumentID");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(e => e.FileName)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(e => e.Link)
                .IsRequired()
                .HasMaxLength(255);

            // Contract
            builder.Property<Guid>("_contractId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("ContractID")
                .IsRequired();

            builder.HasOne(o => o.Contract)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey("_contractId");

            builder.HasIndex("_contractId")
                .HasName("IX_ContractDocument_ContractID");

            // CreatedBy
            builder.Property<long>("_createdById")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("CreatedById")
                .IsRequired();

            builder.HasOne(o => o.CreatedBy)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey("_createdById");

            builder.HasIndex("_createdById")
                .HasName("IX_ContractDocument_CreatedById");

            // ModifiedBy
            builder.Property<long?>("_modifiedById")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("ModifiedById")
                .IsRequired(false);

            builder.HasOne(o => o.ModifiedBy)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey("_modifiedById");

            builder.HasIndex("_modifiedById")
                .HasName("IX_ContractDocument_ModifiedById");
        }
    }
}
