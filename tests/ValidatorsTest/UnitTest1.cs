using FluentValidation.TestHelper;
using Moq;
using Tesouraria.Application.UseCases.Commands.Auth.Register.Member;
using Tesouraria.Domain.Exceptions;
using Tesouraria.Domain.Repositories.User;
using Tesouraria.Domain.Services.Logged;

namespace ValidatorsTest;

public class UnitTest1
{
    [Fact]
    public async Task Success()
    {
        // Arrange
        var mockUser = new Mock<ILoggedUser>();
        mockUser.Setup(u => u.User()).Returns((Guid.NewGuid(), Guid.NewGuid()));

        var mockRepo = new Mock<IUserReadRepository>();
        mockRepo.Setup(r => r.ExistsUserWithName(It.IsAny<string>(), It.IsAny<Guid>(), default))
            .ReturnsAsync(true); // Simula nome já cadastrado

        var validator = new RegisterMemberValidator();

        var command = new RegisterMemberCommand
        {
            Name = "John Doe",
            InviteCode = Guid.NewGuid().ToString(),
            Password = "ValidPass123!"
        };

        // Act
        var result = await validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Name)
            .WithErrorMessage(ResourceMessagesException.NAME_ALREADY_REGISTERED);
    }
}