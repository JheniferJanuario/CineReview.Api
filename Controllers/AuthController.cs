using Microsoft.AspNetCore.Mvc;
using CineReview.Api.Services;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDTO dto)
    {
        // Apenas exemplo: normalmente aqui você validaria a senha no banco
        var token = _authService.GerarToken(dto.Email);
        return Ok(new { Token = token });
    }
}
