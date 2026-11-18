using Microsoft.AspNetCore.Mvc;
using Agenda.Api.Services;
using Agenda.Api.DTOs.Auth;
using Microsoft.AspNetCore.Authorization;

namespace Agenda.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;
    public AuthController(AuthService authService)
    {
        _authService = authService;
    }
    
    [AllowAnonymous]
    [HttpPost("cadastro")]
    public async Task<ActionResult<CadastroResponse>> Cadastrar(
        [FromBody] CadastroRequest request)
    {
        var resposta = await _authService.CadastrarAsync(request);

        return StatusCode(StatusCodes.Status201Created, resposta);
    }
    
}