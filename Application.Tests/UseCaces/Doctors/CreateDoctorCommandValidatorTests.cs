using FluentAssertions;
using FluentValidation.TestHelper;
using MedicalSchedulingBackend.Application.UseCases.Doctors.CreateDoctor;

namespace Application.Tests.UseCaces.Doctors;

public class CreateDoctorCommandValidatorTests
{
    private readonly CreateDoctorCommandValidator _validator;

    public CreateDoctorCommandValidatorTests()
    {
        _validator = new CreateDoctorCommandValidator();
    }

    [Theory]
    [InlineData("", "12344", "pediatra", "79993447777", "guilherme.ped@gmail.com", "O Nome é obrigatório")]
    [InlineData("guilherme", "123456789123456895555544", "pediatra", "79993447777", "guilherme.ped@gmail.com", "A licença deve ter no máximo 20 caracteres")]
    [InlineData("guilherme", "123333", "pediatra", "", "guilherme.ped@gmail.com", "O telefone é obrigatório")]
    [InlineData("guilherme", "123333", "pediatra", "79995224522", "", "O email é obrigatório")]
    [InlineData("guilherme", "123333", "pediatra", "79995224522", "12334teste", "O email informado não é válido")]
    public void Validade_ShouldReturnError_WhenIsNotValid(string name, 
                                                          string licenseNumber, 
                                                          string specialty, 
                                                          string phone, 
                                                          string email,
                                                          string expectedMessage)
    {
        var model = new CreateDoctorCommand(name, licenseNumber, specialty, phone, email);
        var result = _validator.TestValidate(model);
        result.Errors.Should().Contain(e => e.ErrorMessage == expectedMessage);
    }
}
