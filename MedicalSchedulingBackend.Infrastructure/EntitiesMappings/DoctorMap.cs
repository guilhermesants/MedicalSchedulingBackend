using MedicalSchedulingBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalSchedulingBackend.Infrastructure.EntitiesMappings;

internal sealed class DoctorMap : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Id)
               .HasColumnName("id")
               .ValueGeneratedOnAdd();

        builder.Property(a => a.UserId)
               .HasColumnName("user_id");

        builder.Property(a => a.Name)
               .HasColumnName("name");

        builder.Property(a => a.LicenseNumber)
               .HasColumnName("license_number");

        builder.Property(a => a.Specialty)
               .HasColumnName("specialty");

        builder.Property(a => a.Phone)
               .HasColumnName("phone");

        builder.Property(a => a.Email)
               .HasColumnName("email");

        builder.HasMany(a => a.Availabilities)
            .WithOne(x => x.Doctor)
            .HasForeignKey(x => x.DoctorId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable("doctors", "public");
    }
}
