using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DomainModel = SP.Contract.Domains.AggregatesModel.Contract.Entities;

namespace SP.Contract.Persistence.Configurations
{
    internal class ContractConfiguration : IEntityTypeConfiguration<DomainModel.Contract>
    {
        public void Configure(EntityTypeBuilder<DomainModel.Contract> builder)
        {
            builder.ToTable("Contract");

            builder.HasKey(e => e.Id)
                .HasName("PK_ContractID");

            builder.Property(e => e.Id).HasColumnName("ContractID");

            builder.Property(e => e.Number)
                .IsRequired()
                .HasMaxLength(255);

            // ContractStatus
            builder.Property<int>("_contractStatusId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("ContractStatusID")
                .IsRequired();

            builder.HasOne(o => o.ContractStatus)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey("_contractStatusId");

            builder.HasIndex("_contractStatusId")
                .HasName("IX_Contract_ContractStatusID");

            // ContractType
            builder.Property<int>("_contractTypeId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("ContractTypeID")
                .IsRequired();

            builder.HasOne(o => o.ContractType)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey("_contractTypeId");

            builder.HasIndex("_contractTypeId")
                .HasName("IX_Contract_ContractTypeID");

            // CustomerOrganization
            builder.Property<long>("_customerOrganizationId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("CustomerOrganizationID")
                .IsRequired();

            builder.HasOne(o => o.CustomerOrganization)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey("_customerOrganizationId");

            builder.HasIndex("_customerOrganizationId")
                .HasName("IX_Contract_CustomerOrganizationID");

            // ContractorOrganization
            builder.Property<long>("_contractorOrganizationId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("ContractorOrganizationID")
                .IsRequired();

            builder.HasOne(o => o.ContractorOrganization)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey("_contractorOrganizationId");

            builder.HasIndex("_contractorOrganizationId")
                .HasName("IX_Contract_ContractorOrganizationID");

            // ModifiedBy
            builder.Property<long?>("_modifiedById")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("ModifiedBy")
                .IsRequired(false);

            builder.HasOne(o => o.ModifiedBy)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey("_modifiedById");

            builder.HasIndex("_modifiedById")
                .HasName("IX_Contract_ModifiedBy");

            // CreatedBy
            builder.Property<long>("_createdById")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("CreatedBy")
                .IsRequired();

            builder.HasOne(o => o.CreatedBy)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey("_createdById");

            builder.HasIndex("_createdById")
                .HasName("IX_Contract_CreatedBy");
        }
    }
}
