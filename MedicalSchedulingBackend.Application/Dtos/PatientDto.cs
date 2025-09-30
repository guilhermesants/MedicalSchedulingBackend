namespace MedicalSchedulingBackend.Application.Dtos;

public class PatientDto
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
