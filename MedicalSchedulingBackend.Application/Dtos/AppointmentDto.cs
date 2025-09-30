namespace MedicalSchedulingBackend.Application.Dtos;

public class AppointmentDto
{
    public string DoctorName { get; set; } = string.Empty;
    public string Specialty { get; set; } =string.Empty;
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public string Status { get; set; } = string.Empty;
}
