namespace MedicalSchedulingBackend.Application.Dtos;

public class AvailabilitiesDto
{
    public long Id { get; set; }
    public long DoctorId { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
}
