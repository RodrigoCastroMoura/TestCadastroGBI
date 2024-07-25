using AutoMapper;
using GbiTestCadastro.Domain.Entities;
using GbiTestCadastro.Domain.Repositories.Sql;
using GbiTestCadastro.Dto.Usuarios;
using GbiTestCadastro.Infra.Mappers.GbiTestCadastroProfile;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;
using System;
using GbiTestCadastro.Application.Usecases.Usuarios.Delete;

namespace GbiTestCadastro.Test.Unit.Application.Usecases
{
    [TestClass]
    public class UsuarioDeleteUsecasesTests
    {
        private readonly Mock<IUsuarioRepository> iUsuarioRepositoryMock;
        private readonly IMapper mapper;

        public UsuarioDeleteUsecasesTests()
        {
            iUsuarioRepositoryMock = new Mock<IUsuarioRepository>();
            var config = new MapperConfiguration(cfg => cfg.AddProfile(new UsuariosProfile()));
            mapper = config.CreateMapper();
        }

        [TestMethod]
        public async Task SHOULD_DELETE_USUARIO()
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

            iUsuarioRepositoryMock.Setup(x => x.DeleteAsync(It.IsAny<int>()))
                .Returns(Task.CompletedTask);

            iUsuarioRepositoryMock.Setup(x => x.Get(1))
                .ReturnsAsync(usuario);

            var usuarioCreateUsecases = new UsuarioDeleteUsecases(iUsuarioRepositoryMock.Object);
            #endregion

            #region Act
            var result = await usuarioCreateUsecases.Execute(1);
            #endregion

            #region Assert

            Assert.IsTrue(result.Success);
            Assert.IsNull(result.Message);

            #endregion
        }

    }
}
