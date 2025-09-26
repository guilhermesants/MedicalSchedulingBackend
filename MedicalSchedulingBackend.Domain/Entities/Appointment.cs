namespace MedicalSchedulingBackend.Domain.Entities;

public class Appointment
{
    public long Id { get; set; }
    public long PatientId { get; set; }
    public long DoctorId { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly Time { get; set; }
    public int StatusId { get; set; }
    public virtual Patient Patient { get; set; } = null!;
    public virtual Doctor Doctor { get; set; } = null!;
    public AppointmentStatus Status { get; set; } = null!;
}
