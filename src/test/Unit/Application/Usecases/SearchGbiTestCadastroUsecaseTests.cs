using GbiTestCadastro.Application.Usecases.SearchGbiTestCadastro;
using GbiTestCadastro.Domain.Repositories.MongoDb;
using GbiTestCadastro.Dto.GbiTestCadastro;
using Common.Core.Dto.Search;
using ErrorOr;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GbiTestCadastro.Test.Unit.Application.Usecases;

[TestClass]
public class SearchGbiTestCadastroUsecaseTests : UsecaseFixture
{
    [TestMethod]
    public async Task SHOULD_SEARCH_BOILERPLATES()
    {
        #region Arrange
        var boilerplateDto = new GbiTestCadastroCreateDto("Test Name", CrossCutting.Enums.GbiTestCadastroType.Azure);
        var boilerplateEntity = GbiTestCadastro.Domain.Entities.GbiTestCadastro.Create(boilerplateDto.Name, boilerplateDto.GbiTestCadastroType);
        boilerplateEntity.Id = Guid.NewGuid().ToString();
        var boilerplateList = new List<GbiTestCadastro.Domain.Entities.GbiTestCadastro> { boilerplateEntity };

        var boilerplateRepositoryMongoDB = new Mock<IGbiTestCadastroProjectionRepository>();
        boilerplateRepositoryMongoDB.Setup(x => x.Get(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((boilerplateList.Count, boilerplateList));

        var listGbiTestCadastroUsecase = new SearchGbiTestCadastroUsecase(_mapper, boilerplateRepositoryMongoDB.Object);
        #endregion

        #region Act
        var boilerplatesResult = await listGbiTestCadastroUsecase.Execute(new GbiTestCadastroSearchFilterDto(), default);
        #endregion

        #region Assert
        boilerplatesResult.Should().NotBeNull();
        boilerplatesResult.Should().BeOfType<ErrorOr<PagedResultDto<GbiTestCadastroDto>>>();
        boilerplateList.LongCount().Should().Be(boilerplatesResult.Value.Total);
        boilerplateList.Count.Should().Be(boilerplatesResult.Value.Count);
        boilerplateDto.Name.Should().Be(boilerplatesResult.Value.Items.First().Name);
        #endregion
    }
}
