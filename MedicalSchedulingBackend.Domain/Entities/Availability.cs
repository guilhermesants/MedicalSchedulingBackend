namespace MedicalSchedulingBackend.Domain.Entities;

public class Availability
{
    public long Id { get; set; }
    public long DoctorId { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public bool Available { get; set; }
    public Doctor Doctor { get; set; } = null!;
    public ICollection<Appointment> Appointments { get; set; } = null!;
}
