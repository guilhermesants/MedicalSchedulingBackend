using Cortex.Mediator.Queries;
using MedicalSchedulingBackend.Application.Dtos;

namespace MedicalSchedulingBackend.Application.UseCases.AppointmentStatus.GetAppointmentStatus;

public sealed class AppointmentStatusQueryHandler(IUnitOfWork uow) : IQueryHandler<AppointmentStatusQuery, IEnumerable<AppointmentStatusDto>>
{
    private readonly IUnitOfWork _uow = uow;

    public async Task<IEnumerable<AppointmentStatusDto>> Handle(AppointmentStatusQuery query, CancellationToken cancellationToken)
    {
        var result = await _uow.AppointmentStatusRepository.GetAllAsync(cancellationToken);

        return result.Select(x => new AppointmentStatusDto
        {
            Id = x.Id,
            Name = x.Name
        });
    }
}
