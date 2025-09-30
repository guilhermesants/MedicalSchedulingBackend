namespace MedicalSchedulingBackend.Application.Dtos;

public class UpdateAvailabilityDto
{
    public DateOnly Date { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
}
