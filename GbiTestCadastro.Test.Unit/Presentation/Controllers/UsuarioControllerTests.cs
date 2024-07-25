using AutoMapper;
using GbiTestCadastro.Api.Controllers.v1;
using GbiTestCadastro.Application.Usecases.Usuarios.Create;
using GbiTestCadastro.Application.Usecases.Usuarios.Delete;
using GbiTestCadastro.Application.Usecases.Usuarios.Read;
using GbiTestCadastro.Application.Usecases.Usuarios.Update;
using GbiTestCadastro.Domain.Data;
using GbiTestCadastro.Domain.Entities;
using GbiTestCadastro.Domain.Repositories.Sql;
using GbiTestCadastro.Dto.Usuarios;
using GbiTestCadastro.Infra.Mappers.GbiTestCadastroProfile;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GbiTestCadastro.Test.Unit.Presentation.Controllers
{
    [TestClass]
    public  class UsuarioControllerTests
    {
        private readonly UsuarioController usuarioController;
        private readonly Mock<IUsuarioRepository> iUsuarioRepositoryMock;
        private readonly Mock<IUsuarioCreateUsecases> iUsuarioCreateUsecases;
        private readonly Mock<IUsuarioReadUsecases> iUsuarioReadUsecasesMock;
        private readonly Mock<IUsuarioUpdateUsecases> iUsuarioUpdateUsecasesMock;
        private readonly Mock<IUsuarioDeleteUsecases> iUsuarioDeleteUsecasesMock;


        private readonly IMapper mapper;
        public UsuarioControllerTests()
        {
            
            iUsuarioRepositoryMock = new Mock<IUsuarioRepository>();
            iUsuarioCreateUsecases = new Mock<IUsuarioCreateUsecases>();
            iUsuarioReadUsecasesMock = new Mock<IUsuarioReadUsecases>();
            iUsuarioUpdateUsecasesMock = new Mock<IUsuarioUpdateUsecases>();
            iUsuarioDeleteUsecasesMock = new Mock<IUsuarioDeleteUsecases>();

            var config = new MapperConfiguration(cfg => cfg.AddProfile(new UsuariosProfile()));
            mapper = config.CreateMapper();

            usuarioController = new UsuarioController(iUsuarioCreateUsecases.Object
                , iUsuarioReadUsecasesMock.Object
                , iUsuarioUpdateUsecasesMock.Object
                , iUsuarioDeleteUsecasesMock.Object);

        }

        [TestMethod]
        public async Task SHOULD_CONTROLLERS_ADD_USUARIO_ERROR_SENHA()
        {
            #region Arrange

            var dto = new UsuarioCreateDto
            {
                Nome = "Rodrigo Moura",
                Email = "rodrigo@teste.com",
                Username = "rodrigo.moura",
                Senha = "123456",
            };

            var usuario = mapper.Map<Usuario>(dto);
            usuario.Id = 1;
            usuario.Data = DateTime.Now;

            var serviceResponse = new ServiceResponse<Usuario> { Message = "A senha deve ter entre 6 e 100 caracteres.", Success = false };

            iUsuarioCreateUsecases.Setup(x => x.Execute(dto))
                .ReturnsAsync(serviceResponse);

            iUsuarioRepositoryMock.Setup(x => x.Get(1))
                .ReturnsAsync(usuario);

            #endregion

            #region Act
            var result = await usuarioController.Create(dto);
            #endregion

            #region Assert

            Assert.AreEqual("A senha deve ter entre 6 e 100 caracteres.", ((Microsoft.AspNetCore.Mvc.ObjectResult)result.Result).Value);

            #endregion
        }

        [TestMethod]
        public async Task SHOULD_CONTROLLERS_ADD_USUARIO()
        {
            #region Arrange

            var dto = new UsuarioCreateDto
            {
                Nome = "Rodrigo Moura",
                Email = "rodrigo@teste.com",
                Username = "rodrigo.moura",
                Senha = "12345",
            };

            var usuario = mapper.Map<Usuario>(dto);
            usuario.Id = 1;
            usuario.Data = DateTime.Now;

            var serviceResponse = new ServiceResponse<Usuario> { Data = usuario, Success = true };

            iUsuarioCreateUsecases.Setup(x => x.Execute(dto))
                .ReturnsAsync(serviceResponse);

            iUsuarioRepositoryMock.Setup(x => x.Get(1))
                .ReturnsAsync(usuario);

            #endregion

            #region Act
            var result = await usuarioController.Create(dto);
            #endregion

            #region Assert

            
            Assert.AreEqual(dto.Email, ((GbiTestCadastro.Domain.Entities.Usuario)((Microsoft.AspNetCore.Mvc.ObjectResult)result.Result).Value).Email);

            #endregion
        }

        [TestMethod]
        public async Task SHOULD_CONTROLLERS_GET_USUARIO()
        {
            #region Arrange

            var usuarioDto = new UsuarioDto
            {
                Id = 1,
                Nome = "Rodrigo Moura",
                Email = "rodrigo@teste.com",
                Username = "rodrigo.moura",
                Senha = "12345",
                Data = DateTime.Now
            };

            var serviceResponse = new ServiceResponse<UsuarioDto> { Data = usuarioDto, Success = true };
            iUsuarioReadUsecasesMock.Setup(x => x.Execute(1)).ReturnsAsync(serviceResponse);
            #endregion

            #region Act
            var result = await usuarioController.GetById(1);
            #endregion

            #region Assert

            Assert.IsNotNull(((Microsoft.AspNetCore.Mvc.ObjectResult)result.Result).Value);
            Assert.AreEqual(usuarioDto.Id, ((GbiTestCadastro.Dto.Usuarios.UsuarioDto)((Microsoft.AspNetCore.Mvc.ObjectResult)result.Result).Value).Id);

            #endregion
        }

        [TestMethod]
        public async Task SHOULD_CONTROLLERS_GET_USUARIO_NOT_FOUND()
        {
            #region Arrange

            UsuarioDto usuarioDto = null;
            
            var serviceResponse = new ServiceResponse<UsuarioDto> { Data = usuarioDto, Success = true };
            iUsuarioReadUsecasesMock.Setup(x => x.Execute(1)).ReturnsAsync(serviceResponse);
            #endregion

            #region Act
            var result = await usuarioController.GetById(1);
            #endregion

            #region Assert

            Assert.IsNull(((Microsoft.AspNetCore.Mvc.ObjectResult)result.Result).Value);
           
            #endregion
        }

        [TestMethod]
        public async Task SHOULD_CONTROLLERS_GETALL_USUARIO()
        {
            #region Arrange

            var dto = new List<UsuarioDto>
            {
                new UsuarioDto {
                    Id = 1,
                    Nome = "Rodrigo Moura",
                    Email = "rodrigo@teste.com",
                    Username = "rodrigo.moura",
                    Senha = "123456",
                    Data = DateTime.Now
                },
                new UsuarioDto {
                    Id = 1,
                    Nome = "Rodrigo Castro",
                    Email = "rodrigo.castro@teste.com",
                    Username = "rodrigo.castro",
                    Senha = "123456",
                    Data = DateTime.Now
                }
            };

            var pagedResponse = new PagedResponse<UsuarioDto>(dto, dto.Count);
            var serviceResponse = new ServiceResponse<PagedResponse<UsuarioDto>> { Data = pagedResponse, Success = true };

            iUsuarioReadUsecasesMock.Setup(x => x.Execute()).ReturnsAsync(serviceResponse);
            #endregion

            #region Act
            var result = await usuarioController.GetAll();
            #endregion

            #region Assert

            Assert.IsTrue(((GbiTestCadastro.Domain.Data.PagedResponse<GbiTestCadastro.Dto.Usuarios.UsuarioDto>)((Microsoft.AspNetCore.Mvc.ObjectResult)result.Result).Value).Success);

            Assert.AreEqual(dto.Count, ((GbiTestCadastro.Domain.Data.PagedResponse<GbiTestCadastro.Dto.Usuarios.UsuarioDto>)((Microsoft.AspNetCore.Mvc.ObjectResult)result.Result).Value).TotalCount);

            #endregion
        }

        [TestMethod]
        public async Task SHOULD_CONTROLLERS_GETALL_USUARIO_NOT_FOUND()
        {
            #region Arrange

            List<UsuarioDto> dto = new List<UsuarioDto>();

            var pagedResponse = new PagedResponse<UsuarioDto>(dto, dto.Count);
            var serviceResponse = new ServiceResponse<PagedResponse<UsuarioDto>> { Data = pagedResponse, Success = false };

            iUsuarioReadUsecasesMock.Setup(x => x.Execute()).ReturnsAsync(serviceResponse);
            #endregion

            #region Act
            var result = await usuarioController.GetAll();
            #endregion

            #region Assert

            Assert.IsNull(((Microsoft.AspNetCore.Mvc.ObjectResult)result.Result).Value);

            #endregion
        }

        [TestMethod]
        public async Task SHOULD_CONTROLLERS_UPDATE_USUARIO_ERROR_SENHA()
        {
            #region Arrange

            var dto = new UsuarioUpdateDto
            {
                Id = 1,
                Nome = "Rodrigo Moura",
                Email = "rodrigo@teste.com",
                Username = "rodrigo.moura",
                Senha = "123456",
            };

            var usuario = mapper.Map<Usuario>(dto);
            usuario.Id = 1;
            usuario.Data = DateTime.Now;

            var serviceResponse = new ServiceResponse<Usuario> { Message = "A senha deve ter entre 6 e 100 caracteres.", Success = false };

            iUsuarioUpdateUsecasesMock.Setup(x => x.Execute(dto))
                .ReturnsAsync(serviceResponse);

            iUsuarioRepositoryMock.Setup(x => x.Get(1))
                .ReturnsAsync(usuario);

            #endregion

            #region Act
            var result = await usuarioController.Update(dto);
            #endregion

            #region Assert

            Assert.AreEqual("A senha deve ter entre 6 e 100 caracteres.", ((Microsoft.AspNetCore.Mvc.ObjectResult)result.Result).Value);

            #endregion
        }

        [TestMethod]
        public async Task SHOULD_CONTROLLERS_UPDATE_USUARIO()
        {
            #region Arrange

            var dto = new UsuarioUpdateDto
            {
                Nome = "Rodrigo Moura",
                Email = "rodrigo@teste.com",
                Username = "rodrigo.moura",
                Senha = "12345",
            };

            var usuario = mapper.Map<Usuario>(dto);
            usuario.Id = 1;
            usuario.Data = DateTime.Now;

            var serviceResponse = new ServiceResponse<Usuario> { Data = usuario, Success = true };

            iUsuarioUpdateUsecasesMock.Setup(x => x.Execute(dto))
                .ReturnsAsync(serviceResponse);

            iUsuarioRepositoryMock.Setup(x => x.Get(1))
                .ReturnsAsync(usuario);

            #endregion

            #region Act
            var result = await usuarioController.Update(dto);
            #endregion

            #region Assert


            Assert.AreEqual(dto.Nome, ((GbiTestCadastro.Domain.Entities.Usuario)((Microsoft.AspNetCore.Mvc.ObjectResult)result.Result).Value).Nome);

            #endregion
        }

        [TestMethod]
        public async Task SHOULD_CONTROLLERS_DELETE_USUARIO()
        {
            #region Arrange

            var usuario = new Usuario
            {
                Id = 1,
                Nome = "Rodrigo Moura",
                Email = "rodrigo@teste.com",
                Username = "rodrigo.moura",
                Senha = "12345",
                Data = DateTime.Now,
            };
            var serviceResponse = new ServiceResponse<Usuario> { Success = true };

            iUsuarioDeleteUsecasesMock.Setup(x => x.Execute(1))
                .ReturnsAsync(serviceResponse);

          
            #endregion

            #region Act
            var result = await usuarioController.Delete(1);
            #endregion

            #region Assert


            Assert.AreEqual(200, ((Microsoft.AspNetCore.Mvc.StatusCodeResult)result.Result).StatusCode);

            #endregion
        }
    }
}
