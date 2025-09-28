using Cortex.Mediator;
using MedicalSchedulingBackend.Application.Dtos;
using MedicalSchedulingBackend.Application.Security.Roles;
using MedicalSchedulingBackend.Application.UseCases.Doctors.CreateDoctor;
using MedicalSchedulingBackend.Application.UseCases.Doctors.GetDoctor;
using MedicalSchedulingBackend.Presentation.Attributes;
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
}
