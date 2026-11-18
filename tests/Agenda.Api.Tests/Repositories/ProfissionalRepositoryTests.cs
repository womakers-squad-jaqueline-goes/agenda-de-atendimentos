using Microsoft.Data.Sqlite;
using Agenda.Api.Data;
using Microsoft.EntityFrameworkCore;
using Agenda.Api.Repositories;
using Agenda.Api.Models;
using Agenda.Api.Exceptions;

namespace Agenda.Api.Tests.Repositories;

public class ProfissionalRepositoryTests : IDisposable
{
    private readonly SqliteConnection conexao;
    private readonly AgendaDbContext contexto;

    public ProfissionalRepositoryTests()
    {
        conexao = new SqliteConnection("Data Source=:memory:");
        conexao.Open();

        var options = new DbContextOptionsBuilder<AgendaDbContext>()
            .UseSqlite(conexao)
            .Options;

        contexto = new AgendaDbContext(options);
        contexto.Database.Migrate();
    }

    public void Dispose()
    {
        contexto.Dispose();
        conexao.Dispose();
    }

    [Fact]
    public async Task AdicionarAsync_ComEmailDuplicado_DeveLancarEmailJaCadastradoException()
    {
        var repository = new ProfissionalRepository(contexto);

        var primeiraProfissional = new Profissional
        {
            Nome = "Ana",
            Email = "ana@example.test",
            SenhaHash = "hash-ficticio"
        };

        await repository.AdicionarAsync(primeiraProfissional);

        var segundaProfissional = new Profissional
        {
            Nome = "Maria",
            Email = "ana@example.test",
            SenhaHash = "outro-hash-ficticio"
        };    

        await Assert.ThrowsAsync<EmailJaCadastradoException>(
            () => repository.AdicionarAsync(segundaProfissional)
        );
    }
}