using Agenda.Api.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Agenda.Api.Repositories;
using Agenda.Api.Models;
using Microsoft.AspNetCore.Identity;
using Agenda.Api.Services;
using Agenda.Api.DTOs.Auth;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Security.Cryptography.X509Certificates;
using Agenda.Api.Exceptions;

namespace Agenda.Api.Tests.Services;

public class AuthServiceTests : IDisposable
{
    private readonly SqliteConnection conexao;
    private readonly AgendaDbContext contexto;

    public AuthServiceTests()
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
    public async Task CadastrarAsync_ComDadosValidos_DeveSalvarHashERetornarCadastro()
    {
        var repository = new ProfissionalRepository(contexto);
        var passwordHasher = new PasswordHasher<Profissional>();
        var service = new AuthService(repository, passwordHasher);
        var request = new CadastroRequest
        {
            Nome = "Ana",
            Email = "ana@example.test",
            Senha = "Teste123!"
        };
        var resposta = await service.CadastrarAsync(request);

        contexto.ChangeTracker.Clear();

        var profissionalSalva = await contexto.Profissionais.SingleAsync();

        Assert.True(profissionalSalva.Id > 0);
        Assert.Equal(profissionalSalva.Id, resposta.Id);
        Assert.Equal(request.Nome, profissionalSalva.Nome);
        Assert.Equal(request.Email, profissionalSalva.Email);
        Assert.Equal(profissionalSalva.Nome, resposta.Nome);
        Assert.Equal(profissionalSalva.Email, resposta.Email);
        Assert.NotEqual(request.Senha, profissionalSalva.SenhaHash);

        var resultado = passwordHasher.VerifyHashedPassword(
            profissionalSalva,
            profissionalSalva.SenhaHash,
            request.Senha);

        Assert.Equal(PasswordVerificationResult.Success, resultado);
    }
    [Fact]
    public async Task CadastrarAsync_ComEmailJaCadastrado_DeveLancarExcecao()
    {
        var repository = new ProfissionalRepository(contexto);
        var passwordHasher = new PasswordHasher<Profissional>();
        var service = new AuthService(repository, passwordHasher);

        var primeiroRequest = new CadastroRequest
        {
            Nome = "Ana",
            Email = "ana@example.test",
            Senha = "Teste123!"
        };

        await service.CadastrarAsync(primeiroRequest);

        var segundoRequest = new CadastroRequest
        {
            Nome = "Maria",
            Email = "ANA@example.test",
            Senha = "Outra123!"
        };

        await Assert.ThrowsAsync<EmailJaCadastradoException>(
() => service.CadastrarAsync(segundoRequest));

        Assert.Equal(1, await contexto.Profissionais.CountAsync());
    }
}
