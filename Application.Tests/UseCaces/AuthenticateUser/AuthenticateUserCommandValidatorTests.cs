using FluentAssertions;
using FluentValidation.TestHelper;
using MedicalSchedulingBackend.Application.UseCases.Auth.Login;

namespace Application.Tests.UseCaces.AuthenticateUser;

public class AuthenticateUserCommandValidatorTests
{
    private readonly AuthenticateUserCommandValidator _validator;

    public AuthenticateUserCommandValidatorTests()
    {
        _validator = new AuthenticateUserCommandValidator();
    }

    [Fact]
    public void Validate_ShouldReturnError_WhenUsernameIsEmpty()
    {
        var model = new AuthenticateUserCommand("", "admin");
        var result = _validator.TestValidate(model);

        var error = result.Errors.First(e => e.PropertyName == nameof(model.User));
        error.Should().NotBeNull();
        result.Errors.Should().Contain(e => e.ErrorMessage == "O login é obrigatório");
    }

    [Fact]
    public void Validate_ShouldReturnError_WhenPasswordIsEmpty()
    {
        var model = new AuthenticateUserCommand("admin", "");
        var result = _validator.TestValidate(model);

        var error = result.Errors.First(e => e.PropertyName == nameof(model.Password));
        error.Should().NotBeNull();
        result.Errors.Should().Contain(e => e.ErrorMessage == "A senha é obrigatória");
    }

    [Theory]
    [InlineData("", "1234", "O login é obrigatório")]
    [InlineData("admin", "", "A senha é obrigatória")]
    public void Validate_ShouldReturnError_WhenLoginOrPasswordIsEmpty(string login, string password, string expectedMessage)
    {
        var model = new AuthenticateUserCommand(login, password);
        var result = _validator.TestValidate(model);
        result.Errors.Should().Contain(e => e.ErrorMessage == expectedMessage);
    }
}