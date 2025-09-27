using Cortex.Mediator.Commands;
using MedicalSchedulingBackend.Application.Common.Exceptions;
using MedicalSchedulingBackend.Application.Dtos;
using MedicalSchedulingBackend.Application.Interface.Security;

namespace MedicalSchedulingBackend.Application.UseCases.Auth.Login;

public sealed class AuthenticateUserCommandHandler(IUnitOfWork uow, ITokenProvider tokenProvider) : ICommandHandler<AuthenticateUserCommand, AuthDto>
{
    private readonly IUnitOfWork _uow = uow;
    private readonly ITokenProvider _tokenProvider = tokenProvider;

    public async Task<AuthDto> Handle(AuthenticateUserCommand command, CancellationToken cancellationToken)
    {
        var user = await _uow.UserRepository.GetByUserAsync(command.User, cancellationToken)
                   ?? throw new NotFoundException("Usuário não encontrado");

        if (!BCrypt.Net.BCrypt.Verify(command.Password, user.Password))
            throw new UnauthorizedException("Credenciais inválidas");

        var token = _tokenProvider.GenerateToken(user);

        user.LastLogin = DateTime.UtcNow;
        await _uow.CommitAsync(cancellationToken);

        return new AuthDto
        {
            AccessToken = token
        };
    }
}
