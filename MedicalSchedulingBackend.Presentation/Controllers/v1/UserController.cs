using Cortex.Mediator;
using MedicalSchedulingBackend.Application.Dtos;
using MedicalSchedulingBackend.Application.UseCases.Users.GetUser;
using MedicalSchedulingBackend.Presentation.Attributes;
using MedicalSchedulingBackend.Presentation.Extensions;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MedicalSchedulingBackend.Presentation.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class UserController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    [AuthorizeRoles]
    [SwaggerOperation(summary: "Obter usuário", description: "Responsável por obter dados do usuário authenticado")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get(CancellationToken cancellationToken = default)
    {
        var response = await _mediator.SendQueryAsync<GetUserQuery, UserDto>(new GetUserQuery(User.GetUserId()!.Value), cancellationToken);
        return Ok(response);
    }

}
