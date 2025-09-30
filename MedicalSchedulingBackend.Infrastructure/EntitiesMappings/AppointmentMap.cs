using MedicalSchedulingBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalSchedulingBackend.Infrastructure.EntitiesMappings;

internal sealed class AppointmentMap : IEntityTypeConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Id)
               .HasColumnName("id")
               .ValueGeneratedOnAdd();

        builder.Property(a => a.PatientId)
               .HasColumnName("patient_id");

        builder.Property(a => a.AvailabilityId)
               .HasColumnName("availability_id");

        builder.Property(a => a.Time)
               .HasColumnName("time");

        builder.Property(a => a.StatusId)
               .HasColumnName("status_id");

        builder.HasIndex(a => new { a.AvailabilityId, a.Time })
               .IsUnique()
               .HasDatabaseName("uq_active_appointment")
               .HasFilter("status_id = 1");

        builder.ToTable("appointments", "public");
    }
}
