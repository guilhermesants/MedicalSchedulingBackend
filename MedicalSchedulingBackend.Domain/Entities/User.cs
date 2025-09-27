namespace MedicalSchedulingBackend.Domain.Entities;

public class User
{
    public long Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public DateTime? LastLogin { get; set; }
    public bool Active { get; set; }
    public int RoleId { get; set; }
    public virtual Role Role { get; set; } = null!;
    public virtual ICollection<Doctor> Doctors { get; set; } = null!;
    public virtual ICollection<Patient> Patients { get; set; } = null!;
}
