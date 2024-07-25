using GbiTestCadastro.Dto.GbiTestCadastro;
using GbiTestCadastro.Infra.Persistence.MongoDb.Repositories;
using GbiTestCadastro.Test.Integration.Shared;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GbiTestCadastro.Test.Integration.Infra.Persistence.MongoDb.Repositories;

[TestClass]
public class GbiTestCadastroProjectionRepositoryTests : InfraBaseTests
{
    [TestMethod]
    public async Task SHOULD_UPDATE_BOILERPLATE()
    {
        #region Arrange
        var newGbiTestCadastroDto = new GbiTestCadastroCreateDto("Repository Update Test - Created", CrossCutting.Enums.GbiTestCadastroType.AWS);
        var newGbiTestCadastro = Domain.Entities.GbiTestCadastro.Create(newGbiTestCadastroDto.Name, newGbiTestCadastroDto.GbiTestCadastroType);

        var boilerplateRepositoryMongoDb = new GbiTestCadastroProjectionRepository(MongoDatabase);

        await boilerplateRepositoryMongoDb.Insert(newGbiTestCadastro, CancellationToken.None);
        #endregion

        #region Act
        var updateGbiTestCadastroName = "Repository Update Test - Updated";
        var updateGbiTestCadastroDto = new GbiTestCadastroCreateDto(updateGbiTestCadastroName, newGbiTestCadastro.GbiTestCadastroType);
        var updateGbiTestCadastro = Domain.Entities.GbiTestCadastro.Create(updateGbiTestCadastroDto.Name, updateGbiTestCadastroDto.GbiTestCadastroType);
        updateGbiTestCadastro.Id = newGbiTestCadastro.Id;

        await boilerplateRepositoryMongoDb.Update(updateGbiTestCadastro, CancellationToken.None);
        #endregion

        #region Assert
        var checkGbiTestCadastro = await boilerplateRepositoryMongoDb.GetById(updateGbiTestCadastro.Id);

        updateGbiTestCadastro.Id.Should().Be(checkGbiTestCadastro.Id);
        updateGbiTestCadastro.Name.Should().Be(checkGbiTestCadastro.Name);
        updateGbiTestCadastro.GbiTestCadastroType.Should().Be(checkGbiTestCadastro.GbiTestCadastroType); 
        #endregion
    }

    [TestMethod]
    public async Task SHOULD_NOT_GET_BOILERPLATE()
    {
        #region Arrange
        var boilerplateRepositoryMongoDb = new GbiTestCadastroProjectionRepository(MongoDatabase);
        #endregion

        #region Act
        var boilerplate = await boilerplateRepositoryMongoDb.GetById("Id doesn´t exists");
        #endregion

        #region Assert
        boilerplate.Should().BeNull(); 
        #endregion
    }
}
