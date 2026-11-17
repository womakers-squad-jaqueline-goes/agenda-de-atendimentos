using Agenda.Api.Data;
using Agenda.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Api.Repositories;

public class ProfissionalRepository : IProfissionalRepository
{
    private readonly AgendaDbContext _contexto;

    public ProfissionalRepository(AgendaDbContext contexto)
    {
        _contexto = contexto;
    }

    public async Task<Profissional?> BuscarPorEmailAsync(string email)
    {
        var emailNormalizado = email.Trim().ToLowerInvariant();

        return await _contexto.Profissionais
        .FirstOrDefaultAsync(profissional => profissional.Email ==  emailNormalizado);
    }

    public async Task AdicionarAsync(Profissional profissional)
    {
        await _contexto.Profissionais.AddAsync(profissional);
        await _contexto.SaveChangesAsync();
    }
}