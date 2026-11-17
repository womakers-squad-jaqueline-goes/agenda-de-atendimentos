using Microsoft.Data.Sqlite;
using Agenda.Api.Data;
using Microsoft.EntityFrameworkCore;
using Agenda.Api.Models;
using System.Security.Cryptography.X509Certificates;

namespace Agenda.Api.Tests.Data;

public class AgendaDbContextTests : IDisposable
{
    private readonly SqliteConnection conexao;
    private readonly AgendaDbContext contexto;

    public AgendaDbContextTests()
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
    public void SalvarProfissionais_ComMesmoEmail_DeveFalhar()
    {
        var primeiraProfissional = new Profissional
        {
            Nome = "Ana",
            Email = "ana@example.com",
            SenhaHash = "hash-ficticio-para-teste"
        };

        contexto.Profissionais.Add(primeiraProfissional);
        contexto.SaveChanges();

        var segundaProfissional = new Profissional
        {
            Nome = "Maria",
            Email = "ana@example.com",
            SenhaHash = "outro-hash-ficticio"
        };

        contexto.Profissionais.Add(segundaProfissional);

        var excecao = Assert.Throws<DbUpdateException>(
            () => contexto.SaveChanges());

        var erroSqlite = Assert.IsType<SqliteException>(
            excecao.InnerException);

        Assert.Equal(2067, erroSqlite.SqliteExtendedErrorCode);
    }

    [Fact]
    public void SalvarExpedientes_ComMesmaProfissionalEDia_DeveFalhar()
    {
        var profissional = new Profissional
        {
            Nome = "Ana",
            Email = "ana@example.com",
            SenhaHash = "hash-ficticio-para-teste"
        };

        contexto.Profissionais.Add(profissional);
        contexto.SaveChanges();

        var primeiroExpediente = new Expediente
        {
            ProfissionalId = profissional.Id,
            DiaDaSemana = DayOfWeek.Monday,
            Inicio = new TimeOnly(13, 0),
            Fim = new TimeOnly(18, 0)
        };

        contexto.Expedientes.Add(primeiroExpediente);
        contexto.SaveChanges();

        var segundoExpediente = new Expediente
        {
            ProfissionalId = profissional.Id,
            DiaDaSemana = DayOfWeek.Monday,
            Inicio = new TimeOnly(9, 0),
            Fim = new TimeOnly(12, 0)
        };

        contexto.Expedientes.Add(segundoExpediente);

        var excecao = Assert.Throws<DbUpdateException>(
            () => contexto.SaveChanges());

        var erroSqlite = Assert.IsType<SqliteException>(
            excecao.InnerException);

        Assert.Equal(2067, erroSqlite.SqliteExtendedErrorCode);
    }

    [Fact]
    public void ExcluirCliente_ComAgendamento_DeveFalhar()
    {
        var profissional = new Profissional
        {
            Nome = "Janete",
            Email = "janete@example.com",
            SenhaHash = "hash-ficticio-para-teste"
        };

        contexto.Profissionais.Add(profissional);
        contexto.SaveChanges();

        var cliente = new Cliente
        {
            Nome = "Juliana",
            Telefone = "(51) 9999-9999",
            ProfissionalId = profissional.Id
        };

        contexto.Clientes.Add(cliente);
        contexto.SaveChanges();

        var servico = new Servico
        {
            Nome = "Pé e mão",
            DuracaoMinutos = 60,
            Preco = 70m,
            ProfissionalId = profissional.Id
        };

        contexto.Servicos.Add(servico);
        contexto.SaveChanges();

        var agendamento = new Agendamento
        {
            ProfissionalId = profissional.Id,
            ClienteId = cliente.Id,
            ServicoId = servico.Id,
            Inicio = new DateTime(2026, 11, 17, 10, 0, 0),
            Fim = new DateTime(2026, 11, 17, 11, 0, 0),
            PrecoAplicado = servico.Preco,
            Status = StatusAgendamento.Agendado
        };

        contexto.Agendamentos.Add(agendamento);
        contexto.SaveChanges();
        contexto.ChangeTracker.Clear();
        contexto.Clientes.Remove(cliente);

        var excecao = Assert.Throws<DbUpdateException>(
    () => contexto.SaveChanges());

        var erroSqlite = Assert.IsType<SqliteException>(
        excecao.InnerException);

        Assert.Equal(19, erroSqlite.SqliteErrorCode);
        Assert.Contains("FOREIGN KEY constraint failed", erroSqlite.Message);
    }

    [Fact]
    public void ExcluirServico_ComAgendamento_DeveFalhar()
    {
        var profissional = new Profissional
        {
            Nome = "Janete",
            Email = "janete@example.com",
            SenhaHash = "hash-ficticio-para-teste"
        };

        contexto.Profissionais.Add(profissional);
        contexto.SaveChanges();

        var cliente = new Cliente
        {
            Nome = "Juliana",
            Telefone = "(51) 9999-9999",
            ProfissionalId = profissional.Id
        };

        contexto.Clientes.Add(cliente);
        contexto.SaveChanges();

        var servico = new Servico
        {
            Nome = "Pé e mão",
            DuracaoMinutos = 60,
            Preco = 70m,
            ProfissionalId = profissional.Id
        };

        contexto.Servicos.Add(servico);
        contexto.SaveChanges();

        var agendamento = new Agendamento
        {
            ProfissionalId = profissional.Id,
            ClienteId = cliente.Id,
            ServicoId = servico.Id,
            Inicio = new DateTime(2026, 11, 17, 10, 0, 0),
            Fim = new DateTime(2026, 11, 17, 11, 0, 0),
            PrecoAplicado = servico.Preco,
            Status = StatusAgendamento.Agendado
        };

        contexto.Agendamentos.Add(agendamento);
        contexto.SaveChanges();
        contexto.ChangeTracker.Clear();
        contexto.Servicos.Remove(servico);

        var excecao = Assert.Throws<DbUpdateException>(
    () => contexto.SaveChanges());

        var erroSqlite = Assert.IsType<SqliteException>(
        excecao.InnerException);

        Assert.Equal(19, erroSqlite.SqliteErrorCode);
        Assert.Contains("FOREIGN KEY constraint failed", erroSqlite.Message);
    }
}
