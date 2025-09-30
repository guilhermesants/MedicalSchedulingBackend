using Cortex.Mediator;
using MedicalSchedulingBackend.Application.Dtos;
using MedicalSchedulingBackend.Application.Security.Roles;
using MedicalSchedulingBackend.Application.UseCases.Doctors.CreateDoctor;
using MedicalSchedulingBackend.Application.UseCases.Doctors.GetDoctor;
using MedicalSchedulingBackend.Application.UseCases.Patients.CreatePatient;
using MedicalSchedulingBackend.Application.UseCases.Patients.GetPatient;
using MedicalSchedulingBackend.Presentation.Attributes;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MedicalSchedulingBackend.Presentation.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class PatientController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    [AuthorizeRoles(Role.Admin)]
    [SwaggerOperation(summary: "Cadastrar Paciente", description: "Responsável por cadastrar um paciente e usuário")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] CreatePatientCommand command, CancellationToken cancellationToken = default)
    {
        var response = await _mediator.SendCommandAsync<CreatePatientCommand, long>(command, cancellationToken);
        return CreatedAtAction(nameof(Get), new { id = response }, null);
    }

    [HttpGet("{id}")]
    [AuthorizeRoles]
    [SwaggerOperation(summary: "Obter Paciente", description: "Responsável por obter informções de um paciente pelo id")]
    [ProducesResponseType(typeof(PatientDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get(long id, CancellationToken cancellationToken = default)
    {
        var response = await _mediator.SendQueryAsync<GetPatientQuery, PatientDto>(new GetPatientQuery(id), cancellationToken);
        return Ok(response);
    }
}
