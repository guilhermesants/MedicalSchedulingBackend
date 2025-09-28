using FluentAssertions;
using MedicalSchedulingBackend.Application.Common.Exceptions;
using MedicalSchedulingBackend.Application.Interface.Security;
using MedicalSchedulingBackend.Application.UseCases.Auth.Login;
using MedicalSchedulingBackend.Application.UseCases.Doctors.CreateDoctor;
using MedicalSchedulingBackend.Domain.Entities;
using MedicalSchedulingBackend.Domain.Interfaces.Repositories;
using Moq;
using System.Linq.Expressions;

namespace Application.Tests.UseCaces.Doctors;

public class CreateDoctorCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> _uow;
    private readonly Mock<IDoctorRepository> _doctorRepository;
    private readonly CreateDoctorCommandHandler _handler;

    public CreateDoctorCommandHandlerTests()
    {
        _uow = new Mock<IUnitOfWork>();
        _doctorRepository = new Mock<IDoctorRepository>();
        _uow.Setup(u => u.DoctorRepository).Returns(_doctorRepository.Object);
        _handler = new CreateDoctorCommandHandler(_uow.Object);
    }

    [Fact(DisplayName = "Simula o cadastro de um médico ja existente na base, devendo lançar excecao")]
    public async Task Handler_ShouldThrowBadRequestException_WhenEmailisRegistered()
    {
        var command = new CreateDoctorCommand
        (
            "Dr. Test",
            "teste@exemplo.com",
            "123456",
            "Cardiologia",
             "999999999"
        );

        _doctorRepository
            .Setup(r => r.FirstOrDefaultAsync(
                It.IsAny<Expression<Func<Doctor, bool>>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Doctor { Email = command.Email });

        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<BadRequestException>()
                 .WithMessage("Já existe usuário cadastrado com este email");
    }

}
