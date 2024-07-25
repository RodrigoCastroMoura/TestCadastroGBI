using AutoMapper;
using GbiTestCadastro.Application.Usecases.Usuarios.Create;
using GbiTestCadastro.Domain.Entities;
using GbiTestCadastro.Domain.Repositories.Sql;
using GbiTestCadastro.Dto.Usuarios;
using GbiTestCadastro.Infra.Mappers.GbiTestCadastroProfile;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace GbiTestCadastro.Test.Unit.Application.Usecases;

[TestClass]
public  class UsuarioCreateUsecaseTests
{
    private readonly Mock<IUsuarioRepository> iUsuarioRepositoryMock;
    private readonly IMapper mapper;

    public UsuarioCreateUsecaseTests()
    {
        iUsuarioRepositoryMock = new Mock<IUsuarioRepository>();
        var config = new MapperConfiguration(cfg => cfg.AddProfile(new UsuariosProfile()));
        mapper = config.CreateMapper();
    }

    [TestMethod]
    public async Task SHOULD_CREATE_USUARIO()
    {
        #region Arrange
 
        var dto = new UsuarioCreateDto
        {
            Nome = "Rodrigo Moura",
            Email = "rodrigo@teste.com",
            Username ="rodrigo.moura",
            Senha = "123456"
        };
        var usuario = mapper.Map<Usuario>(dto);
        usuario.Id = 1;
        usuario.Data = DateTime.Now;

        iUsuarioRepositoryMock.Setup(repo => repo.Add(It.IsAny<Usuario>()))
            .Returns(Task.CompletedTask);

        iUsuarioRepositoryMock.Setup(x => x.Get(1))
            .ReturnsAsync(usuario);

        var usuarioCreateUsecases = new UsuarioCreateUsecases(iUsuarioRepositoryMock.Object, mapper);
        #endregion

        #region Act
        var result = await usuarioCreateUsecases.Execute(dto);
        #endregion

        #region Assert

        Assert.IsTrue(result.Success);
        Assert.IsNull(result.Message);

        #endregion
    }

    [TestMethod]
    public async Task SHOULD_CREATE_USUARIO_ERROR_SENHA()
    {
        #region Arrange
        var dto = new UsuarioCreateDto
        {
            Nome = "Rodrigo Moura",
            Email = "rodrigo@teste.com",
            Username = "rodrigo.moura",
            Senha = "12345"
        };
        var usuario = mapper.Map<Usuario>(dto);
        usuario.Id = 1;
        usuario.Data = DateTime.Now;

        iUsuarioRepositoryMock.Setup(repo => repo.Add(It.IsAny<Usuario>()))
            .Returns(Task.CompletedTask);

        iUsuarioRepositoryMock.Setup(x => x.Get(1))
            .ReturnsAsync(usuario);

        var usuarioCreateUsecases = new UsuarioCreateUsecases(iUsuarioRepositoryMock.Object, mapper);
        #endregion

        #region Act
        var result = await usuarioCreateUsecases.Execute(dto);
        #endregion

        #region Assert

        Assert.IsFalse(result.Success);
        Assert.IsNotNull(result.Message);
       
        #endregion
    }
}
