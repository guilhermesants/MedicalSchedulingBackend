namespace MedicalSchedulingBackend.Domain.Entities;

public class AppointmentStatus
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<Appointment> Appointments { get; set; } = null!;
}
