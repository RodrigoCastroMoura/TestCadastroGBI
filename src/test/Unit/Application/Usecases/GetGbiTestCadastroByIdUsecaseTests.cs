using GbiTestCadastro.Application.Usecases.GetGbiTestCadastroById;
using GbiTestCadastro.Domain.Repositories.MongoDb;
using GbiTestCadastro.Dto.GbiTestCadastro;
using ErrorOr;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GbiTestCadastro.Test.Unit.Application.Usecases;

[TestClass]
public class GetGbiTestCadastroByIdUsecaseTests : UsecaseFixture
{        
    [TestMethod]
    public async Task SHOULD_GET_BOILERPLATE()
    {
        #region Arrange
        var boilerplateDto = new GbiTestCadastroCreateDto("Test Name", CrossCutting.Enums.GbiTestCadastroType.Azure);
        var boilerplateEntity = GbiTestCadastro.Domain.Entities.GbiTestCadastro.Create(boilerplateDto.Name, boilerplateDto.GbiTestCadastroType);
        boilerplateEntity.Id = Guid.NewGuid().ToString();

        var boilerplateRepositoryMongoDB = new Mock<IGbiTestCadastroProjectionRepository>();
        boilerplateRepositoryMongoDB.Setup(x => x.GetById(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(boilerplateEntity);

        var getGbiTestCadastroByIdUsecase = new GetGbiTestCadastroByIdUsecase(_mapper, boilerplateRepositoryMongoDB.Object);
        #endregion

        #region Act
        var boilerplateDtoResult = await getGbiTestCadastroByIdUsecase.Execute(GbiTestCadastroGetByIdFilterDto.From(boilerplateEntity.Id), default);
        #endregion

        #region Assert
        boilerplateDtoResult.Should().NotBeNull();
        boilerplateDtoResult.Should().BeOfType<ErrorOr<GbiTestCadastroDto>>();
        boilerplateDto.Name.Should().Be(boilerplateDtoResult.Value.Name);
        #endregion

    }

    [TestMethod]
    public async Task SHOULD_BOILERPLATE_NOT_FOUND()
    {
        #region Arrange
        var boilerplateRepositoryMongoDB = new Mock<IGbiTestCadastroProjectionRepository>();
        boilerplateRepositoryMongoDB.Setup(x => x.GetById(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()));

        var getGbiTestCadastroByIdUsecase = new GetGbiTestCadastroByIdUsecase(_mapper, boilerplateRepositoryMongoDB.Object);
        #endregion

        #region Act
        var boilerplate = await getGbiTestCadastroByIdUsecase.Execute(GbiTestCadastroGetByIdFilterDto.From("Id doesn´t exists"), default);
        #endregion

        #region Assert
        boilerplate.IsError.Should().BeTrue();
        boilerplate.FirstError.Should().Match<Error>(x => x.Description == "GbiTestCadastro não encontrado");
        #endregion
    }
}
