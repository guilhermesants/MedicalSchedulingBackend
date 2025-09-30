using System.Text.Json.Serialization;

namespace MedicalSchedulingBackend.Application.Dtos;

public class UserDto
{
    public long Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public DateTime? LastLogin { get; set; }
    public bool Active { get; set; }
    public string PerfilName { get; set; } = string.Empty;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DoctorDto? DoctorDto { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public PatientDto? PatientDto { get; set; }
}
