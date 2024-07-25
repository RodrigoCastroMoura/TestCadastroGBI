using GbiTestCadastro.Test.Shared.Dto;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GbiTestCadastro.Test.Unit.Domain.Entities;

[TestClass]
public class GbiTestCadastroDomainTests
{
    [TestMethod]
    public void SHOULD_CREATE_BOILERPLATE()
    {
        var boilerplateDTO = CreateGbiTestCadastroDefaultTestDto.GetDefault();
        var boilerplate = GbiTestCadastro.Domain.Entities.GbiTestCadastro.Create(boilerplateDTO.Name, boilerplateDTO.GbiTestCadastroType);

        boilerplate.Name.Should().Be(boilerplateDTO.Name);
        boilerplate.GbiTestCadastroType.Should().Be(boilerplateDTO.GbiTestCadastroType);
    }


    [TestMethod]
    public void SHOULD_CREATE_BOILERPLATE_WITH_EMPTY_CONSTRUCTOR()
    {
        var id = Guid.NewGuid().ToString();
        var boilerplate = GbiTestCadastro.Domain.Entities.GbiTestCadastro.Create(id);

        boilerplate.Id.Should().Be(id);
        boilerplate.OwnerId.Should().Be(null);
    }
}
