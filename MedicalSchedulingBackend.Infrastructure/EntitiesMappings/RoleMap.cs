using MedicalSchedulingBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalSchedulingBackend.Infrastructure.EntitiesMappings;

internal sealed class RoleMap : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

        builder.Property(r => r.Name)
                .HasColumnName("name")
                .ValueGeneratedOnAdd();

        builder.HasMany(r => r.Users)
                .WithOne(r => r.Role)
                .HasForeignKey(r => r.RoleId);

        builder.ToTable("Roles", "public");
    }
}
