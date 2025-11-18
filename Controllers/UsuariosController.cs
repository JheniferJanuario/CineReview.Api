using Microsoft.AspNetCore.Mvc;
using CineReview.Api.Data;
using CineReview.Api.Models;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly CineReviewContext _context;

    public UsuariosController(CineReviewContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetUsuarios()
    {
        return Ok(_context.Usuarios.ToList());
    }
}
