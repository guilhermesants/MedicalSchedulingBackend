using Cortex.Mediator.Queries;
using MedicalSchedulingBackend.Application.Dtos;

namespace MedicalSchedulingBackend.Application.UseCases.Doctors.GetDoctor;

public record GetDoctorQuery(long Id) : IQuery<DoctorDto>;
