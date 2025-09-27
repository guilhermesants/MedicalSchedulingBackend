using Cortex.Mediator;
using MedicalSchedulingBackend.Application.Dtos;
using MedicalSchedulingBackend.Application.Security.Roles;
using MedicalSchedulingBackend.Application.UseCases.AppointmentStatus.GetAppointmentStatus;
using MedicalSchedulingBackend.Presentation.Attributes;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MedicalSchedulingBackend.Presentation.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class AppointmentStatusController(IMediator meditor) : ControllerBase
{
    private readonly IMediator _meditor = meditor;

    [HttpGet]
    [AuthorizeRoles]
    [SwaggerOperation(summary: "Obter status de agendamento", description: "Retorna uma lista de status disponíveis")]
    [ProducesResponseType(typeof(AppointmentStatusDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAppointmentStatus(CancellationToken cancellationToken = default)
    {
        var response = await _meditor.SendQueryAsync<AppointmentStatusQuery, IEnumerable<AppointmentStatusDto>>(
            new AppointmentStatusQuery(), cancellationToken
            );

        return Ok(response);
    }
}
