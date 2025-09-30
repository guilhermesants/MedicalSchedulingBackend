using Cortex.Mediator;
using MedicalSchedulingBackend.Application.Security.Roles;
using MedicalSchedulingBackend.Application.UseCases.Appointments.CreateAppointment;
using MedicalSchedulingBackend.Presentation.Attributes;
using MedicalSchedulingBackend.Presentation.Extensions;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MedicalSchedulingBackend.Presentation.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class AppointmentController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    [AuthorizeRoles(Role.Patient)]
    [SwaggerOperation(summary: "Cadastrar agenda", description: "Responsável pelo paciente poder cadastrar uma agenda")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] CreateAppointmentCommand command, CancellationToken cancellationToken = default)
    {
        var commandWithIdUserPatiente = command with { UserId = User.GetUserId()!.Value };
        await _mediator.SendCommandAsync<CreateAppointmentCommand, Unit>(commandWithIdUserPatiente, cancellationToken);
        return NoContent();
    }
}
