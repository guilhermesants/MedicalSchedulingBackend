using MedicalSchedulingBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalSchedulingBackend.Infrastructure.EntitiesMappings;

internal sealed class AppointmentsStatusMap : IEntityTypeConfiguration<AppointmentStatus>
{
    public void Configure(EntityTypeBuilder<AppointmentStatus> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

        builder.Property(a => a.Name)
                .HasColumnName("name");

        builder.HasMany(a => a.Appointments)
               .WithOne(a => a.AppointmentStatus)
               .HasForeignKey(a => a.StatusId);

        builder.ToTable("appointment_status", "public");
    }
}
