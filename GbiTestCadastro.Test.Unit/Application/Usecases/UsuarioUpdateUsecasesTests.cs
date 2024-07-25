using AutoMapper;
using GbiTestCadastro.Application.Usecases.Usuarios.Update;
using GbiTestCadastro.Domain.Entities;
using GbiTestCadastro.Domain.Repositories.Sql;
using GbiTestCadastro.Dto.Usuarios;
using GbiTestCadastro.Infra.Mappers.GbiTestCadastroProfile;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace GbiTestCadastro.Test.Unit.Application.Usecases
{
    [TestClass]
    public class UsuarioUpdateUsecasesTests
    {
        private readonly Mock<IUsuarioRepository> iUsuarioRepositoryMock;
        private readonly IMapper mapper;

        public UsuarioUpdateUsecasesTests()
        {
            iUsuarioRepositoryMock = new Mock<IUsuarioRepository>();
            var config = new MapperConfiguration(cfg => cfg.AddProfile(new UsuariosProfile()));
            mapper = config.CreateMapper();
        }

        [TestMethod]
        public async Task SHOULD_UPDATE_USUARIO()
        {
            #region Arrange

            var dto = new UsuarioUpdateDto
            {
                Id = 1,
                Nome = "Rodrigo Castro",
                Email = "rodrigo@teste.com",
                Username = "rodrigo.moura",
                Senha = "123456"
            };

            var usuario = mapper.Map<Usuario>(dto);
            usuario.Data = DateTime.Now;

            iUsuarioRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Usuario>()))
                .Returns(Task.CompletedTask);

            iUsuarioRepositoryMock.Setup(x => x.Get(1))
                .ReturnsAsync(usuario);

            var usuarioCreateUsecases = new UsuarioUpdateUsecases(iUsuarioRepositoryMock.Object, mapper);
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
        public async Task SHOULD_DELETE_USUARIO_ERROR_EMAIL()
        {
            #region Arrange

            var dto = new UsuarioUpdateDto
            {
                Id = 1,
                Nome = "Rodrigo Castro",
                Email = "rodrigo",
                Username = "rodrigo.moura",
                Senha = "123456"
            };

            var usuario = mapper.Map<Usuario>(dto);
            usuario.Data = DateTime.Now;

            iUsuarioRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Usuario>()))
                .Returns(Task.CompletedTask);

            iUsuarioRepositoryMock.Setup(x => x.Get(1))
                .ReturnsAsync(usuario);

            var usuarioCreateUsecases = new UsuarioUpdateUsecases(iUsuarioRepositoryMock.Object, mapper);
            #endregion

            #region Act
            var result = await usuarioCreateUsecases.Execute(dto);
            #endregion

            #region Assert

            Assert.IsFalse(result.Success);
            Assert.IsNotNull(result.Message);
            Assert.AreEqual("O email deve ser válido.", result.Message);

            #endregion
        }
    }
}
