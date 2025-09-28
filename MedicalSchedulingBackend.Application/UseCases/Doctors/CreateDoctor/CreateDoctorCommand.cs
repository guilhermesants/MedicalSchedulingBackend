using Cortex.Mediator;
using Cortex.Mediator.Commands;

namespace MedicalSchedulingBackend.Application.UseCases.Doctors.CreateDoctor;

public sealed record CreateDoctorCommand
(string Name,
string LicenseNumber,
string Specialty,
string Phone,
string Email) : ICommand<long>;
