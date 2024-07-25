using AutoMapper;
using GbiTestCadastro.Application.Usecases.Usuarios.Read;
using GbiTestCadastro.Domain.Entities;
using GbiTestCadastro.Domain.Repositories.Sql;
using GbiTestCadastro.Infra.Mappers.GbiTestCadastroProfile;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GbiTestCadastro.Test.Unit.Application.Usecases
{
    [TestClass]
    public  class UsuarioReadUsecasesTest
    {
        private readonly Mock<IUsuarioRepository> iUsuarioRepositoryMock;
        private readonly IMapper mapper;

        public UsuarioReadUsecasesTest()
        {
            iUsuarioRepositoryMock = new Mock<IUsuarioRepository>();
            var config = new MapperConfiguration(cfg => cfg.AddProfile(new UsuariosProfile()));
            mapper = config.CreateMapper();
        }

        [TestMethod]
        public async Task SHOULD_READ_GET_USUARIO()
        {
            #region Arrange

            var usuario = new Usuario
            {
                Id = 1,
                Nome = "Rodrigo Moura",
                Email = "rodrigo@teste.com",
                Username = "rodrigo.moura",
                Senha = "12345",
                Data = DateTime.Now
            };

            iUsuarioRepositoryMock.Setup(x => x.Get(1)).ReturnsAsync(usuario);

            var usuarioReadUsecases = new UsuarioReadUsecases(iUsuarioRepositoryMock.Object, mapper);
            #endregion

            #region Act
            var result = await usuarioReadUsecases.Execute(1);
            #endregion

            #region Assert

            Assert.IsNotNull(result.Data);
            Assert.AreEqual(usuario.Id, result.Data.Id);

            #endregion
        }

        [TestMethod]
        public async Task SHOULD_READ_GET_USUARIO_NOT_FOUND()
        {
            #region Arrange

            var usuario = new Usuario
            {
                Id = 1,
                Nome = "Rodrigo Moura",
                Email = "rodrigo@teste.com",
                Username = "rodrigo.moura",
                Senha = "12345",
                Data = DateTime.Now
            };

            iUsuarioRepositoryMock.Setup(x => x.Get(2)).ReturnsAsync(usuario);

            var usuarioReadUsecases = new UsuarioReadUsecases(iUsuarioRepositoryMock.Object, mapper);
            #endregion

            #region Act
            var result = await usuarioReadUsecases.Execute(1);
            #endregion

            #region Assert

            Assert.IsNull(result.Data);
            #endregion
        }

        [TestMethod]
        public async Task SHOULD_READ_GETALL_USUARIO()
        {
            #region Arrange

            var usuarios = new List<Usuario>
            {
                new Usuario {
                    Id = 1,
                    Nome = "Rodrigo Moura",
                    Email = "rodrigo@teste.com",
                    Username = "rodrigo.moura",
                    Senha = "123456",
                    Data = DateTime.Now
                },
                new Usuario {
                    Id = 1,
                    Nome = "Rodrigo Castro",
                    Email = "rodrigo.castro@teste.com",
                    Username = "rodrigo.castro",
                    Senha = "123456",
                    Data = DateTime.Now
                }
            };

            

            iUsuarioRepositoryMock.Setup(x => x.GetAll()).ReturnsAsync(usuarios);

            var usuarioReadUsecases = new UsuarioReadUsecases(iUsuarioRepositoryMock.Object, mapper);
            #endregion

            #region Act
            var result = await usuarioReadUsecases.Execute();
            #endregion

            #region Assert

            Assert.IsNotNull(result.Data);

            #endregion
        }

        [TestMethod]
        public async Task SHOULD_READ_GETALL_USUARIO_NOT_FOUND()
        {
            #region Arrange

            List<Usuario> usuarios = null;

            iUsuarioRepositoryMock.Setup(x => x.GetAll()).ReturnsAsync(usuarios);


            var usuarioReadUsecases = new UsuarioReadUsecases(iUsuarioRepositoryMock.Object, mapper);
            #endregion

            #region Act
            var result = await usuarioReadUsecases.Execute();
            #endregion

            #region Assert

            Assert.AreEqual(result.Data.TotalCount,0);
            #endregion
        }
    }
}
