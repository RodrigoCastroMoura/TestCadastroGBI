using GbiTestCadastro.Dto.GbiTestCadastro;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http.Json;

namespace GbiTestCadastro.Test.Integration.Presentation.Api.Conttrollers;

[TestClass]
public class GbiTestCadastroControllerTests : ControllerBaseTests
{
    [TestMethod]
    public async Task CreateGbiTestCadastro()
    {
        var httpClient = WebAppFactory.CreateDefaultClient();

        var boilerplateDTO = new GbiTestCadastroCreateDto("Meu teste integrado", CrossCutting.Enums.GbiTestCadastroType.Azure);
        var response = await httpClient.PostAsJsonAsync("boilerplate", boilerplateDTO);
        response.IsSuccessStatusCode.Should().Be(true);


        //var bodyResponse = await response.Content.ReadAsStringAsync();

        //var boileplateIdDto = await response.Content.ReadAsAsync<IdDTO>();

        //dynamic boileplateIdDto = JsonConvert.DeserializeObject<dynamic>(bodyResponse);

        //var response2 = await httpClient.GetAsync($"boilerplate/{boileplateIdDto.Id}");

        //response2.IsSuccessStatusCode.Should().Be(true);

        //var bodyResponse2 = await response2.Content.ReadAsStringAsync();

        //var boileplateCreated = JsonConvert.DeserializeObject<GbiTestCadastroDTO>(bodyResponse2);

        //boileplateCreated.Should().Be(boilerplateDTO);



    }
}
