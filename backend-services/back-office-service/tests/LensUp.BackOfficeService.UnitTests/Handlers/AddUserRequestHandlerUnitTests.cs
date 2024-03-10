using FluentAssertions;
using LensUp.BackOfficeService.Application.Commands.AddUser;
using LensUp.BackOfficeService.Domain.Entities;
using LensUp.BackOfficeService.Domain.Repositories;
using LensUp.Common.Types.Id;
using Moq;
using Xunit;

namespace LensUp.BackOfficeService.UnitTests.Handlers;

public sealed class AddUserRequestHandlerUnitTests
{
    private readonly Mock<IIdGenerator> idGeneratorMock;
    private readonly Mock<IUserRepository> userRepositoryMock;

    private readonly AddUserRequestHandler uut;

    public AddUserRequestHandlerUnitTests()
    {
        this.idGeneratorMock = new Mock<IIdGenerator>();
        this.userRepositoryMock = new Mock<IUserRepository>();

        this.uut = new AddUserRequestHandler(this.idGeneratorMock.Object, this.userRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_Should_Add_New_User()
    {
        // Arrange
        var request = new AddUserRequest("John");
        var cancellationToken = CancellationToken.None;
        var expectedUserId = Guid.NewGuid().ToString();
        UserEntity? addedUser = null;

        this.idGeneratorMock.
            Setup(x => x.Generate())
            .Returns(expectedUserId);

        this.userRepositoryMock
            .Setup(x => x.AddAsync(It.IsAny<UserEntity>(), It.Is<CancellationToken>(x => x == cancellationToken)))
            .Callback<UserEntity, CancellationToken>((x, _) => addedUser = x)
            .Returns(Task.CompletedTask);

        // Act
        var result = await this.uut.Handle(request, cancellationToken);

        // Assert
        result.Should().NotBeNull().And.Be(expectedUserId);
        addedUser.Should().NotBeNull();
        addedUser!.RowKey.Should().Be(expectedUserId);
        addedUser!.PartitionKey.Should().Be(expectedUserId);
        addedUser!.Name.Should().Be(request.Name);

        this.userRepositoryMock
            .Verify(x => x.AddAsync(It.IsAny<UserEntity>(), It.Is<CancellationToken>(x => x == cancellationToken)), Times.Once);
    }
}
