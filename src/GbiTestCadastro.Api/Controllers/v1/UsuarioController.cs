using GbiTestCadastro.Application.Usecases.Usuarios.Create;
using GbiTestCadastro.Application.Usecases.Usuarios.Delete;
using GbiTestCadastro.Application.Usecases.Usuarios.Read;
using GbiTestCadastro.Application.Usecases.Usuarios.Update;
using GbiTestCadastro.Domain.Data;
using GbiTestCadastro.Domain.Entities;
using GbiTestCadastro.Dto.Usuarios;
using Microsoft.AspNetCore.Mvc;

namespace GbiTestCadastro.Api.Controllers.v1;

[ApiVersion("1.0")]
[Route("Usuario")]
[ApiController]
[Produces("application/json")]
public class UsuarioController :  ControllerBase
{

    private readonly IUsuarioCreateUsecases iUsuarioCreateUsecases;
    private readonly IUsuarioReadUsecases iUsuarioReadUsecases;
    private readonly IUsuarioUpdateUsecases iUsuarioUpdateUsecases;
    private readonly IUsuarioDeleteUsecases iUsuarioDeleteUsecases;


    public UsuarioController(
      IUsuarioCreateUsecases iUsuarioCreateUsecases,
      IUsuarioReadUsecases iUsuarioReadUsecases,
      IUsuarioUpdateUsecases iUsuarioUpdateUsecases,
      IUsuarioDeleteUsecases iUsuarioDeleteUsecases
    )
    {
        this.iUsuarioCreateUsecases = iUsuarioCreateUsecases;
        this.iUsuarioReadUsecases = iUsuarioReadUsecases;
        this.iUsuarioUpdateUsecases = iUsuarioUpdateUsecases;
        this.iUsuarioDeleteUsecases = iUsuarioDeleteUsecases;

    }

    /// <summary>
    /// Criar Cadastro.
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST/Usuario
    ///     {
    ///        "nome": "Nome do usuario",
    ///        "email":"Email do usuario",
    ///        "username":"Username do usuario",
    ///        "senha": "Senha do Usuario"
    ///     }
    ///
    /// </remarks>
    /// <param name="usuarioCreateDto"></param>
    /// <returns>A newly created GbiTestCadastro</returns>
    /// <response code="201">Returns the newly created boilerplate</response>
    [ProducesResponseType(typeof(UsuarioDto), StatusCodes.Status201Created)]
    [HttpPost]
    public async Task<ActionResult<ServiceResponse<Usuario>>> Create([FromBody] UsuarioCreateDto usuarioCreateDto)
    {
        var response = await iUsuarioCreateUsecases.Execute(usuarioCreateDto);

        if (response.Success)
        {
            return CreatedAtAction(nameof(GetById), new { id = response.Data.Id }, response.Data);
        }
        return BadRequest(response.Message);
    }

    /// <summary>
    /// Listar Usuario por ID
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     Get/Usuario/102030
    ///
    /// </remarks>
    /// <param name="id"></param>
    /// <returns>returns a boilerplate</returns>
    /// <response code="200">Returns a boilerplate </response>
    /// <response code="422">If boilerplate not found </response>  
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(UsuarioDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<ServiceResponse<Usuario>>> GetById([FromRoute] int id)
    {
        var response = await iUsuarioReadUsecases.Execute(id);

        if (response.Success)
        {
            return Ok(response.Data);
        }
        return NotFound(response.Message);
    }

    /// <summary>
    /// Listar todos os Usuarios
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     Get/Usuario
    ///
    /// </remarks>
    /// Listar todos os Usuarios
    /// <returns>returns boilerplates</returns>
    /// <response code="200">Returns Usuarios </response> 
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<UsuarioDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ServiceResponse<PagedResponse<UsuarioDto>>>> GetAll()
    {
        var response = await iUsuarioReadUsecases.Execute();
        if (response.Success)
        {
            return Ok(response.Data);
        }
        return BadRequest(response.Message);
    }

    /// <summary>
    /// Alterar Cadastro.
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     Put/Usuario
    ///     {
    ///        "id": "Nome do usuario",
    ///        "nome": "Nome do usuario",
    ///        "email":"Email do usuario",
    ///        "username":"Username do usuario",
    ///        "senha": "Senha do Usuario"
    ///     }
    ///
    /// </remarks>
    /// <param name="usuarioUpdateDto"></param>
    /// <returns>A newly created GbiTestCadastro</returns>
    /// <response code="201">Returns the newly created boilerplate</response>
    [ProducesResponseType(typeof(UsuarioDto), StatusCodes.Status200OK)]
    [HttpPut]
    public async Task<ActionResult<ServiceResponse<Usuario>>> Update([FromBody] UsuarioUpdateDto usuarioUpdateDto)
    {
        var response = await iUsuarioUpdateUsecases.Execute(usuarioUpdateDto);

        if (response.Success)
        {
            return CreatedAtAction(nameof(GetById), new { id = response.Data.Id }, response.Data);
        }
        return BadRequest(response.Message);
    }

    /// <summary>
    /// Deletar Cadastro.
    /// </summary>
    /// <remarks>
    ///     Delete/Usuario/102030
    /// </remarks>
    /// <param name="usuarioUpdateDto"></param>
    /// <returns>A newly created GbiTestCadastro</returns>
    /// <response code="201">Returns the newly created boilerplate</response>
    [ProducesResponseType(typeof(ServiceResponse<Usuario>), StatusCodes.Status200OK)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ServiceResponse<Usuario>>> Delete([FromRoute] int id)
    {
        var response = await iUsuarioDeleteUsecases.Execute(id);

        if (response.Success)
        {
            return Ok();
        }
        return BadRequest(response.Message);
    }


}
