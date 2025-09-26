using MedicalSchedulingBackend.Domain.Entities;
using MedicalSchedulingBackend.Infrastructure.EntitiesMappings;
using Microsoft.EntityFrameworkCore;

namespace MedicalSchedulingBackend.Infrastructure.Context;

internal sealed class MedicalSchedulingContext(DbContextOptions<MedicalSchedulingContext> options) : DbContext(options)
{
    public DbSet<Role> Roles { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new RoleMap());
    }
}
