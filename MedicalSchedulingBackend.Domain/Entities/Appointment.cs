namespace MedicalSchedulingBackend.Domain.Entities;

public class Appointment
{
    public long Id { get; set; }
    public long PatientId { get; set; }
    public long AvailabilityId { get; set; }
    public TimeOnly Time { get; set; }
    public int StatusId { get; set; }
    public virtual Patient Patient { get; set; } = null!;
    public AppointmentStatus AppointmentStatus { get; set; } = null!;
    public Availability Availability { get; set; } = null!;
}
