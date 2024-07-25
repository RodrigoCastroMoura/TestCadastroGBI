using GbiTestCadastro.Application.ExternalServices;
using GbiTestCadastro.Application.Usecases.CreateGbiTestCadastro;
using GbiTestCadastro.Domain.Repositories.MongoDb;
using GbiTestCadastro.Domain.Repositories.Sql;
using GbiTestCadastro.Dto.GbiTestCadastro;
using ErrorOr;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GbiTestCadastro.Test.Unit.Application.Usecases;

[TestClass]
public class CreateGbiTestCadastroUsecaseTests : UsecaseFixture
{
    [TestMethod]
    [DataRow("Test Name")]
    [DataRow("Other Name")]
    public async Task SHOULD_CREATE_BOILERPLATE(string name)
    {
        #region Arrange
        var messageBroker = new Mock<IMessageBroker>();
        var eventStreaming = new Mock<IEventStreaming>();

        var boilerplateDto = new GbiTestCadastroCreateDto(name, CrossCutting.Enums.GbiTestCadastroType.Azure);
        var boilerplateEntity = GbiTestCadastro.Domain.Entities.GbiTestCadastro.Create(boilerplateDto.Name, boilerplateDto.GbiTestCadastroType);
        boilerplateEntity.Id = Guid.NewGuid().ToString();

        var boilerplateProjectionRepository = new Mock<IGbiTestCadastroProjectionRepository>();
        boilerplateProjectionRepository.Setup(x => x.Insert(It.IsAny<GbiTestCadastro.Domain.Entities.GbiTestCadastro>(), It.IsAny<CancellationToken>()))
            .Callback<GbiTestCadastro.Domain.Entities.GbiTestCadastro, CancellationToken>((boilerplate, _) =>
            {
                boilerplate.Id = boilerplateEntity.Id;
            });

        var boilerplateRepository = new Mock<IGbiTestCadastroRepository>();
        boilerplateRepository.Setup(x => x.Insert(It.IsAny<GbiTestCadastro.Domain.Entities.GbiTestCadastro>(), It.IsAny<CancellationToken>()))
            .Callback<GbiTestCadastro.Domain.Entities.GbiTestCadastro, CancellationToken>((boilerplate, _) =>
            {
                boilerplate.Id = boilerplateEntity.Id;
            });

        var createGbiTestCadastroUsecase = new CreateGbiTestCadastroUsecase(
            _mapper, messageBroker.Object, eventStreaming.Object,
            boilerplateProjectionRepository.Object, boilerplateRepository.Object
        );
        #endregion

        #region Act
        var boilerplateId = await createGbiTestCadastroUsecase.Execute(boilerplateDto, default);
        #endregion

        #region Assert
        boilerplateId.Should().NotBeNull();
        boilerplateId.Should().BeOfType<ErrorOr<GbiTestCadastroDto>>();
        boilerplateId.Value.Id.Should().Be(boilerplateEntity.Id);
        boilerplateEntity.Name.Should().Be(boilerplateDto.Name);
        boilerplateEntity.GbiTestCadastroType.Should().Be(boilerplateDto.GbiTestCadastroType);

        messageBroker.Verify(x => x.SendMessage(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Dictionary<string, object>>(), It.IsAny<CancellationToken>()), Times.Once);
        messageBroker.Verify(x => x.PublishMessage(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Dictionary<string, object>>(), It.IsAny<CancellationToken>()), Times.Once);
        eventStreaming.Verify(x => x.SendEvent(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once());
        boilerplateProjectionRepository.Verify(x => x.Insert(It.IsAny<GbiTestCadastro.Domain.Entities.GbiTestCadastro>(), It.IsAny<CancellationToken>()), Times.Once());
        boilerplateRepository.Verify(x => x.Insert(It.IsAny<GbiTestCadastro.Domain.Entities.GbiTestCadastro>(), It.IsAny<CancellationToken>()), Times.Once());
        #endregion
    }

    [TestMethod]
    [DataRow(null)]
    public async Task SHOULD_NOT_CREATE_BOILERPLATE_WITH_NULL_NAME(string name)
    {
        #region Arrange
        var messageBroker = new Mock<IMessageBroker>();
        var eventStreaming = new Mock<IEventStreaming>();

        var boilerplateDto = new GbiTestCadastroCreateDto(name, CrossCutting.Enums.GbiTestCadastroType.Azure);
        var boilerplateEntity = GbiTestCadastro.Domain.Entities.GbiTestCadastro.Create(boilerplateDto.Name, boilerplateDto.GbiTestCadastroType);
        boilerplateEntity.Id = Guid.NewGuid().ToString();

        var boilerplateProjectionRepository = new Mock<IGbiTestCadastroProjectionRepository>();
        boilerplateProjectionRepository.Setup(x => x.Insert(It.IsAny<GbiTestCadastro.Domain.Entities.GbiTestCadastro>(), It.IsAny<CancellationToken>()))
            .Callback<GbiTestCadastro.Domain.Entities.GbiTestCadastro, CancellationToken>((boilerplate, _) =>
            {
                boilerplate.Id = boilerplateEntity.Id;
            });

        var createGbiTestCadastroUsecase = new CreateGbiTestCadastroUsecase(
           _mapper, messageBroker.Object, eventStreaming.Object, boilerplateProjectionRepository.Object, null
        );
        #endregion

        #region Act
        var boilerplateId = await createGbiTestCadastroUsecase.Execute(boilerplateDto, default);
        #endregion

        #region Assert
        boilerplateId.Should().NotBeNull();
        boilerplateId.Should().BeOfType<ErrorOr<GbiTestCadastroDto>>();
        boilerplateId.IsError.Should().BeTrue();
        boilerplateId.Errors.Count.Should().Be(1);
        boilerplateId.FirstError.Should().Match<Error>(x => x.Description == "'Nome do GbiTestCadastro' must not be empty.");

        messageBroker.Verify(x => x.SendMessage(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Dictionary<string, object>>(), It.IsAny<CancellationToken>()), Times.Never);
        messageBroker.Verify(x => x.PublishMessage(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Dictionary<string, object>>(), It.IsAny<CancellationToken>()), Times.Never);
        eventStreaming.Verify(x => x.SendEvent(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never);
        #endregion
    }

    [TestMethod]
    [DataRow("a")]
    [DataRow("ab")]
    public async Task SHOULD_NOT_CREATE_BOILERPLATE_WITH_INVALID_NAME(string name)
    {
        #region Arrange
        var messageBroker = new Mock<IMessageBroker>();
        var eventStreaming = new Mock<IEventStreaming>();

        var boilerplateDto = new GbiTestCadastroCreateDto(name, CrossCutting.Enums.GbiTestCadastroType.Azure);
        var boilerplateEntity = GbiTestCadastro.Domain.Entities.GbiTestCadastro.Create(boilerplateDto.Name, boilerplateDto.GbiTestCadastroType);
        boilerplateEntity.Id = Guid.NewGuid().ToString();

        var boilerplateProjectionRepository = new Mock<IGbiTestCadastroProjectionRepository>();
        boilerplateProjectionRepository.Setup(x => x.Insert(It.IsAny<GbiTestCadastro.Domain.Entities.GbiTestCadastro>(), It.IsAny<CancellationToken>()))
            .Callback<GbiTestCadastro.Domain.Entities.GbiTestCadastro, CancellationToken>((boilerplate, _) =>
            {
                boilerplate.Id = boilerplateEntity.Id;
            });

        var createGbiTestCadastroUsecase = new CreateGbiTestCadastroUsecase(
           _mapper, messageBroker.Object, eventStreaming.Object, boilerplateProjectionRepository.Object, null
        );
        #endregion

        #region Act
        var boilerplateId = await createGbiTestCadastroUsecase.Execute(boilerplateDto, default);
        #endregion

        #region Assert
        boilerplateId.Should().NotBeNull();
        boilerplateId.Should().BeOfType<ErrorOr<GbiTestCadastroDto>>();
        boilerplateId.IsError.Should().BeTrue();
        boilerplateId.Errors.Count.Should().Be(1);
        boilerplateId.FirstError.Should().Match<Error>(x => x.Description.Contains("'Nome do GbiTestCadastro' must be between 3 and 100 characters"));

        messageBroker.Verify(x => x.SendMessage(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Dictionary<string, object>>(), It.IsAny<CancellationToken>()), Times.Never);
        messageBroker.Verify(x => x.PublishMessage(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Dictionary<string, object>>(), It.IsAny<CancellationToken>()), Times.Never);
        eventStreaming.Verify(x => x.SendEvent(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never);
        #endregion
    }
}
