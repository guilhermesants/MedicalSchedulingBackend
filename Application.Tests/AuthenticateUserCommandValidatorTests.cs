using FluentAssertions;
using FluentValidation.TestHelper;
using MedicalSchedulingBackend.Application.UseCases.Auth.Login;
using System.Text;

namespace Application.Tests;

public class AuthenticateUserCommandValidatorTests
{
    private readonly AuthenticateUserCommandValidator _validator;

    public AuthenticateUserCommandValidatorTests()
    {
        _validator = new AuthenticateUserCommandValidator();
    }

    private static string Normalize(string input) => input.Normalize(NormalizationForm.FormC);

    [Fact]
    public void Validate_ShouldReturnError_WhenUsernameIsEmpty()
    {
        var model = new AuthenticateUserCommand("", "admin");
        var result = _validator.TestValidate(model);

        var error = result.Errors.First(e => e.PropertyName == nameof(model.User));
        error.Should().NotBeNull();
        Normalize(error.ErrorMessage).Should().Be(Normalize("O login � obrigat�rio"));
    }

    [Fact]
    public void Validate_ShouldReturnError_WhenPasswordIsEmpty()
    {
        var model = new AuthenticateUserCommand("admin", "");
        var result = _validator.TestValidate(model);

        var error = result.Errors.First(e => e.PropertyName == nameof(model.Password));
        error.Should().NotBeNull();
        Normalize(error.ErrorMessage).Should().Be(Normalize("A senha � obrigat�ria"));
    }

    [Theory]
    [InlineData("", "1234", "O login � obrigat�rio")]
    [InlineData("admin", "", "A senha � obrigat�ria")]
    public void Validate_ShouldReturnError_WhenLoginOrPasswordIsEmpty(string login, string password, string expectedMessage) 
    {
        var model = new AuthenticateUserCommand(login, password);
        var result = _validator.TestValidate(model);
        result.Errors.Should().Contain(e => Normalize(e.ErrorMessage) == Normalize(expectedMessage));
    }
}