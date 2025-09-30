using Cortex.Mediator.Queries;
using MedicalSchedulingBackend.Application.Dtos;

namespace MedicalSchedulingBackend.Application.UseCases.Patients.GetPatient;

public sealed record GetPatientQuery(long id) : IQuery<PatientDto>;
