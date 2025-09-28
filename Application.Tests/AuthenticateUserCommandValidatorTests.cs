using FluentAssertions;
using FluentValidation.TestHelper;
using MedicalSchedulingBackend.Application.UseCases.Auth.Login;

namespace Application.Tests;

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

        result.ShouldHaveValidationErrorFor(x => x.User)
              .WithErrorMessage("O login � obrigat�rio");
    }

    [Fact]
    public void Validate_ShouldReturnError_WhenPasswordIsEmpty()
    {
        var model = new AuthenticateUserCommand("admin", "");
        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.Password)
              .WithErrorMessage("A senha � obrigat�ria");
    }

    [Theory]
    [InlineData("", "1234", "O login � obrigat�rio")]
    [InlineData("admin", "", "A senha � obrigat�ria")]
    public void Validate_ShouldReturnError_WhenLoginOrPasswordIsEmpty(string login, string password, string expectedMessage) 
    {
        var model = new AuthenticateUserCommand(login, password);
        var result = _validator.TestValidate(model);
        result.Errors.Should().Contain(e => e.ErrorMessage == expectedMessage);
    }
}