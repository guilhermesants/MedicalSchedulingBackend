using Cortex.Mediator;
using MedicalSchedulingBackend.Application.Dtos;
using MedicalSchedulingBackend.Application.UseCases.Auth.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MedicalSchedulingBackend.Presentation.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    [AllowAnonymous]
    [SwaggerOperation(summary: "Autenticar um usúario", description: "Responsável por autenticar um usúario")]
    [ProducesResponseType(typeof(AuthDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Login([FromBody] AuthenticateUserCommand command, CancellationToken cancellationToken = default)
    {
        var response = await _mediator.SendCommandAsync<AuthenticateUserCommand, AuthDto>(command, cancellationToken);
        return Ok(response);
    }
}
