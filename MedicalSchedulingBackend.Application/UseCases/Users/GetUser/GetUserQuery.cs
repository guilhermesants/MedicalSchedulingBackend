using Cortex.Mediator.Queries;
using MedicalSchedulingBackend.Application.Dtos;

namespace MedicalSchedulingBackend.Application.UseCases.Users.GetUser;

public sealed record GetUserQuery(long IdUser) : IQuery<UserDto>;