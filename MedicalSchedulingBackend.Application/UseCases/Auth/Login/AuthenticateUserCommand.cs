using Cortex.Mediator.Commands;
using MedicalSchedulingBackend.Application.Dtos;

namespace MedicalSchedulingBackend.Application.UseCases.Auth.Login;

public sealed record AuthenticateUserCommand(string User, string Password) : ICommand<AuthDto>;
