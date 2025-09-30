using Cortex.Mediator.Commands;

namespace MedicalSchedulingBackend.Application.UseCases.Patients.CreatePatient;

public sealed record CreatePatientCommand
(string Name,
string Cpf,
string Phone,
string Email) : ICommand<long>;
