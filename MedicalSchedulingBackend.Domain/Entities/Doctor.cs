namespace MedicalSchedulingBackend.Domain.Entities;

public class Doctor
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string LicenseNumber { get; set; } = string.Empty;
    public string Specialty { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public virtual User User { get; set; } = null!;
    public virtual ICollection<Availability> Availabilities { get; set; } = null!;
    public ICollection<Appointment> Appointments { get; set; } = null!;
}
