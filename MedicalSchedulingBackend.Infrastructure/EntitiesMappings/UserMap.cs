using MedicalSchedulingBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalSchedulingBackend.Infrastructure.EntitiesMappings;

internal sealed class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Id)
               .HasColumnName("id")
               .ValueGeneratedOnAdd();

        builder.Property(a => a.UserName)
               .HasColumnName("username");
        
        builder.Property(a => a.Password)
               .HasColumnName("password");

        builder.Property(a => a.LastLogin)
               .HasColumnName("last_login");

        builder.Property(a => a.Active)
               .HasColumnName("active");

        builder.Property(a => a.RoleId)
               .HasColumnName("role_id");

        builder.HasMany(a => a.Doctors)
               .WithOne(a => a.User)
               .HasForeignKey(a => a.UserId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable("users", "public");
    }
}

