using Agenda.Api.Data;
using Agenda.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using Agenda.Api.Exceptions;

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
        .FirstOrDefaultAsync(profissional => profissional.Email == emailNormalizado);
    }

    public async Task AdicionarAsync(Profissional profissional)
    {
        try
        {
            await _contexto.Profissionais.AddAsync(profissional);
            await _contexto.SaveChangesAsync();
        }

        catch (DbUpdateException exception) when (
    exception.InnerException is SqliteException erroSqlite
    && erroSqlite.SqliteExtendedErrorCode == 2067
    && erroSqlite.Message.Contains("UNIQUE constraint failed: Profissionais.Email"))
        {
            throw new EmailJaCadastradoException(
                "Este e-mail já está cadastrado.");
        }
    }
}