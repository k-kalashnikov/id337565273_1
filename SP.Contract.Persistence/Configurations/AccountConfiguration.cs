using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SP.Contract.Domains.AggregatesModel.Misc.Entities;

namespace SP.Contract.Persistence.Configurations
{
    internal class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Account");

            builder.HasKey(e => e.Id)
                .HasName("PK_AccountID");

            builder.Property(e => e.Id)
                .HasColumnName("AccountID")
                .ValueGeneratedNever();

            builder.Property(o => o.FirstName)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(o => o.LastName)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(o => o.MiddleName)
                .HasMaxLength(255)
                .IsRequired(false);

            builder.Property(p => p.FullName)
                .HasComputedColumnSql(@"""LastName"" || ' ' || ""FirstName"" || ' ' || COALESCE(""MiddleName"" || ' ', '')");

            // Organization
            builder.Property<long?>("_organizationId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("OrganizationID");

            builder.HasOne(o => o.Organization)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey("_organizationId");

            builder.HasIndex("_organizationId")
                .HasName("IX_Account_OrganizationID");
        }
    }
}
