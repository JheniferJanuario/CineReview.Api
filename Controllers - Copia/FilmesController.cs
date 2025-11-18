using Microsoft.AspNetCore.Mvc;
using CineReview.Api.Data;
using CineReview.Api.Models;

[ApiController]
[Route("api/[controller]")]
public class FilmesController : ControllerBase
{
    private readonly CineReviewContext _context;

    public FilmesController(CineReviewContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetFilmes()
    {
        var filmes = _context.Filmes.ToList();
        return Ok(filmes);
    }

    [HttpPost]
    public IActionResult CriarFilme([FromBody] Filme filme)
    {
        _context.Filmes.Add(filme);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetFilmes), new { id = filme.Id }, filme);
    }
}
