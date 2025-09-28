using FluentAssertions;
using MedicalSchedulingBackend.Application.Common.Exceptions;
using MedicalSchedulingBackend.Application.Interface.Security;
using MedicalSchedulingBackend.Application.UseCases.Auth.Login;
using MedicalSchedulingBackend.Domain.Entities;
using MedicalSchedulingBackend.Domain.Interfaces.Repositories;
using Moq;

namespace Application.Tests.UseCaces.AuthenticateUser;

public class AuthenticateUserCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> _uow;
    private readonly Mock<IUserRepository> _userRepository;
    private readonly Mock<ITokenProvider> _tokenProvider;
    private readonly AuthenticateUserCommandHandler _handler;

    public AuthenticateUserCommandHandlerTests()
    {
        _uow = new Mock<IUnitOfWork>();
        _userRepository = new Mock<IUserRepository>();
        _tokenProvider = new Mock<ITokenProvider>();
        _uow.Setup(u => u.UserRepository).Returns(_userRepository.Object);
        _handler = new AuthenticateUserCommandHandler(_uow.Object, _tokenProvider.Object);
    }

    [Theory]
    [InlineData("teste", "1234", "Usuário não encontrado", "NotFound")]
    [InlineData("admin", "12345", "Credenciais inválidas", "Unauthorized")]
    public async Task Handler_ShouldThrowException_WhenInvalidLoginOrPassword(string username,
                                                                              string password,
                                                                              string expectedMessage,
                                                                              string exceptionType)
    {
        if (exceptionType == "Unauthorized")
        {
            var user = new User
            {
                Id = 1,
                UserName = "admin",
                Password = BCrypt.Net.BCrypt.HashPassword("1234")
            };

            _userRepository
                .Setup(r => r.GetByUserAsync(username, It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);
        }
        else
        {
            _userRepository
                .Setup(r => r.GetByUserAsync(username, It.IsAny<CancellationToken>()))
                .ReturnsAsync((User?)null);
        }

        var command = new AuthenticateUserCommand(username, password);

        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        if (exceptionType == "NotFound")
        {
            await act.Should()
                     .ThrowAsync<NotFoundException>()
                     .WithMessage(expectedMessage);
        }
        else if (exceptionType == "Unauthorized")
        {
            await act.Should()
                     .ThrowAsync<UnauthorizedException>()
                     .WithMessage(expectedMessage);
        }
    }
}
