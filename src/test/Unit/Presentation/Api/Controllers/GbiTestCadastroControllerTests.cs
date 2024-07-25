using GbiTestCadastro.Api.Controllers.v1;
using GbiTestCadastro.Application.Usecases.CreateGbiTestCadastro;
using GbiTestCadastro.Application.Usecases.GetGbiTestCadastroById;
using GbiTestCadastro.Application.Usecases.SearchGbiTestCadastro;
using GbiTestCadastro.Dto.GbiTestCadastro;
using Common.Core.Dto.Search;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GbiTestCadastro.Test.Unit.Presentation.Api.Controllers;

[TestClass]
public class GbiTestCadastroControllerTests
{
    [TestMethod]
    public async Task SHOULD_CREATE_BOILERPLATE()
    {
        #region arrange
        var nameResponse = "Create Async Test";
        var boilerplateResponseDto = new GbiTestCadastroDto(Guid.NewGuid().ToString(), nameResponse, CrossCutting.Enums.GbiTestCadastroType.Azure);
        var createGbiTestCadastroUsecaseMock = new Mock<ICreateGbiTestCadastroUsecase>();
        createGbiTestCadastroUsecaseMock
            .Setup(x => x.Execute(It.IsAny<GbiTestCadastroCreateDto>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(boilerplateResponseDto);

        var boilerplateController = new GbiTestCadastroController(createGbiTestCadastroUsecaseMock.Object, default, default);
        #endregion

        #region act
        var result = await boilerplateController.Create(new GbiTestCadastroCreateDto(nameResponse, CrossCutting.Enums.GbiTestCadastroType.Azure), default);
        #endregion

        #region assert
        result.Should().NotBeNull();
        var createdResult = result.Should().BeOfType<ActionResult<GbiTestCadastroDto>>().Subject;
        var boilerplateResult = (createdResult.Result as ObjectResult).Value.Should().BeAssignableTo<GbiTestCadastroDto>().Subject;
        boilerplateResult.Id.Should().Be(boilerplateResponseDto.Id);
        #endregion
    }

    [TestMethod]
    public async Task SHOULD_LIST_BOILERPLATES()
    {
        #region arrange
        var nameOfGbiTestCadastro = "TEST SHOULD_GET_BOILERPLATES";

        var boilerplateResponseDto = new GbiTestCadastroDto(Guid.NewGuid().ToString(), nameOfGbiTestCadastro, CrossCutting.Enums.GbiTestCadastroType.Azure);
        var listOfGbiTestCadastrosDto = new List<GbiTestCadastroDto> { boilerplateResponseDto };
        var boilerplateGetResponse = new PagedResultDto<GbiTestCadastroDto>(listOfGbiTestCadastrosDto.Count, listOfGbiTestCadastrosDto);

        var listGbiTestCadastroUsecaseMock = new Mock<ISearchGbiTestCadastroUsecase>();
        listGbiTestCadastroUsecaseMock
            .Setup(x => x.Execute(It.IsAny<GbiTestCadastroSearchFilterDto>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(boilerplateGetResponse);

        var boilerplateController = new GbiTestCadastroController(default, listGbiTestCadastroUsecaseMock.Object, default);
        #endregion

        #region act
        var result = await boilerplateController.Search(new GbiTestCadastroSearchFilterDto { Offset = 0, Limit = 10 }, default);
        #endregion

        #region assert
        result.Should().NotBeNull();
        var objectResult = result.Should().BeOfType<ActionResult<PagedResultDto<GbiTestCadastroDto>>>().Subject;
        var boilerplateResult = (objectResult.Result as ObjectResult).Value.Should().BeAssignableTo<PagedResultDto<GbiTestCadastroDto>>().Subject;
        boilerplateResult.Total.Should().Be(listOfGbiTestCadastrosDto.Count);
        boilerplateResult.Items.First().Name.Should().Be(boilerplateResponseDto.Name);
        #endregion
    }

    [TestMethod]
    public async Task SHOULD_GET_BOILERPLATE_BY_ID()
    {
        #region arrange
        var nameOfGbiTestCadastro = "TEST SHOULD_GET_BOILERPLATE_BY_ID";
        var boilerplateDtoResponse = new GbiTestCadastroDto(Guid.NewGuid().ToString(), nameOfGbiTestCadastro, CrossCutting.Enums.GbiTestCadastroType.Azure);

        var getGbiTestCadastroByIdUsecaseMock = new Mock<IGetGbiTestCadastroByIdUsecase>();
        getGbiTestCadastroByIdUsecaseMock
            .Setup(x => x.Execute(It.IsAny<GbiTestCadastroGetByIdFilterDto>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(boilerplateDtoResponse);

        var boilerplateController = new GbiTestCadastroController(default, default, getGbiTestCadastroByIdUsecaseMock.Object);
        #endregion

        #region act
        var result = await boilerplateController.GetById(Guid.NewGuid().ToString(), default);
        #endregion

        #region assert
        result.Should().NotBeNull();
        var okResult = result.Should().BeOfType<ActionResult<GbiTestCadastroDto>>().Subject;
        var boilerplateResult = (okResult.Result as ObjectResult).Value.Should().BeAssignableTo<GbiTestCadastroDto>().Subject;
        boilerplateResult.Name.Should().Be(nameOfGbiTestCadastro);
        #endregion

    }
}
