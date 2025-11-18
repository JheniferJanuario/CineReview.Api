using CineReview.Api.Models;
using CineReview.Api.Data;

namespace CineReview.Api.Services;
public interface IMidiaService
{
    IEnumerable<Filme> ListarFilmes();
}

public class MidiaService : IMidiaService
{
    private readonly CineReviewContext _context;
    public MidiaService(CineReviewContext context)
    {
        _context = context;
    }

    public IEnumerable<Filme> ListarFilmes()
    {
        return _context.Filmes.OrderByDescending(f => f.NotaMedia).ToList();
    }
}
