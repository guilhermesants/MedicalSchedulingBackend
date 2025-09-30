using MedicalSchedulingBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalSchedulingBackend.Infrastructure.EntitiesMappings;

internal sealed class PatientMap : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Id)
               .HasColumnName("id")
               .ValueGeneratedOnAdd();

        builder.Property(a => a.UserId)
               .HasColumnName("user_id");

        builder.Property(a => a.Name)
               .HasColumnName("name");

        builder.Property(a => a.Cpf)
               .HasColumnName("cpf");

        builder.Property(a => a.Phone)
               .HasColumnName("phone");

        builder.Property(a => a.Email)
               .HasColumnName("email");

        builder.ToTable("patients", "public");
    }
}
