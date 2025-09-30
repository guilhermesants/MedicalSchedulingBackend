using Cortex.Mediator;
using MedicalSchedulingBackend.Application.Dtos;
using MedicalSchedulingBackend.Application.Security.Roles;
using MedicalSchedulingBackend.Application.UseCases.Doctors.CreateAvailabilityForDoctor;
using MedicalSchedulingBackend.Application.UseCases.Doctors.CreateDoctor;
using MedicalSchedulingBackend.Application.UseCases.Doctors.GetAvailabilities;
using MedicalSchedulingBackend.Application.UseCases.Doctors.GetDoctor;
using MedicalSchedulingBackend.Application.UseCases.Doctors.UpdateAvailabilityForDoctor;
using MedicalSchedulingBackend.Presentation.Attributes;
using MedicalSchedulingBackend.Presentation.Extensions;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MedicalSchedulingBackend.Presentation.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class DoctorController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    [AuthorizeRoles(Role.Admin)]
    [SwaggerOperation(summary: "Cadastrar Médico", description: "Responsável por cadastrar um médico e usuário")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] CreateDoctorCommand command, CancellationToken cancellationToken = default)
    {
        var response = await _mediator.SendCommandAsync<CreateDoctorCommand, long>(command, cancellationToken);
        return CreatedAtAction(nameof(Get), new { id = response }, null);
    }

    [HttpGet("{id}")]
    [AuthorizeRoles]
    [SwaggerOperation(summary: "Obter Médico", description: "Responsável por obter informções de um médico pelo id")]
    [ProducesResponseType(typeof(DoctorDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get(long id, CancellationToken cancellationToken = default)
    {
        var response = await _mediator.SendQueryAsync<GetDoctorQuery, DoctorDto>(new GetDoctorQuery(id), cancellationToken);
        return Ok(response);
    }

    [HttpPost("availability")]
    [AuthorizeRoles(Role.Doctor)]
    [SwaggerOperation(summary: "Cadastrar Disponibilidade", description: "Responsável por cadastrar um horario disponivel para médico")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateAvailabilityForDoctor([FromBody] CreateAvailabilityForDoctorCommand command, CancellationToken cancellationToken = default)
    {
        var commandWithDocorId = command with { IdUserDoctor = User.GetUserId()!.Value };
        var response = await _mediator.SendCommandAsync<CreateAvailabilityForDoctorCommand, long>(commandWithDocorId, cancellationToken);
        return Created($"api/v1/Doctor/availability?idDoctor={response}", null);
    }

    [HttpGet("availability")]
    [AuthorizeRoles]
    [SwaggerOperation(summary: "Obter Disponibilidades", description: "Responsável por obter disponibiliddae de um determinado médico ou todos")]
    [ProducesResponseType(typeof(IEnumerable<DoctorDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAvailabilities([FromQuery] GetAvailabilitiesQuery query, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.SendQueryAsync<GetAvailabilitiesQuery, IEnumerable<DoctorDto>>(query, cancellationToken);
        return Ok(result);
    }

    [HttpPut("availability/{idAvailability}")]
    [AuthorizeRoles(Role.Doctor)]
    [SwaggerOperation(summary: "Atualizar uma disponibilidades", description: "Responsável por atualizar uma disponibiliade de um médico")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateAvailabilitiy([FromRoute] long idAvailability, [FromBody] UpdateAvailabilityDto updateAvailabilityDto, CancellationToken cancellationToken = default)
    {
        var command = new UpdateAvailabilityForDoctorCommand(idAvailability, updateAvailabilityDto);
        await _mediator.SendCommandAsync<UpdateAvailabilityForDoctorCommand, Unit>(command, cancellationToken);
        return NoContent();
    }
}
