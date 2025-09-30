using MedicalSchedulingBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalSchedulingBackend.Infrastructure.EntitiesMappings;

public class AvailabilityMap : IEntityTypeConfiguration<Availability>
{
    public void Configure(EntityTypeBuilder<Availability> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Id)
               .HasColumnName("id")
               .ValueGeneratedOnAdd();

        builder.Property(a => a.DoctorId)
               .HasColumnName("doctor_id");

        builder.Property(a => a.Date)
               .HasColumnName("date");

        builder.Property(a => a.StartTime)
               .HasColumnName("start_time");

        builder.Property(a => a.EndTime)
               .HasColumnName("end_time");

        builder.Property(a => a.Available)
               .HasColumnName("available")
               .HasDefaultValue(true);

        builder.HasOne(a => a.Doctor)
               .WithMany(a => a.Availabilities)
               .HasForeignKey(a => a.DoctorId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(a => new { a.DoctorId, a.Date, a.StartTime, a.EndTime })
               .IsUnique()
               .HasDatabaseName("uq_doctor_date");

        builder.ToTable("availabilities", "public", tb =>
        {
            tb.HasCheckConstraint("ck_time", "\"start_time\" < \"end_time\"");
        });
    }
}
