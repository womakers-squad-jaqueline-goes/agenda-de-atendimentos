using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Agenda.Api.DTOs.Auth;

namespace Agenda.Api.Tests.Endpoints;

public class AuthControllerTests : IDisposable
{
    private readonly AgendaApiFactory factory;
    private readonly HttpClient client;

    public AuthControllerTests()
    {
        factory = new AgendaApiFactory();
        factory.InicializarBanco();

        client = factory.CreateClient(
            new WebApplicationFactoryClientOptions
            {
                BaseAddress = new Uri("https://localhost"),
                AllowAutoRedirect = false
            });
    }

    public void Dispose()
    {
        client.Dispose();
        factory.Dispose();
    }

    [Fact]
    public async Task Cadastrar_ComDadosValidos_DeveRetornar201()
    {
        var request = new CadastroRequest
        {
            Nome = "Ana",
            Email = "ana@example.test",
            Senha = "Teste123!"
        };

        using var resposta = await client.PostAsJsonAsync(
            "/api/auth/cadastro",
            request);

        Assert.Equal(HttpStatusCode.Created, resposta.StatusCode);

        using var documento = await resposta.Content
            .ReadFromJsonAsync<JsonDocument>();

        Assert.NotNull(documento);

        var cadastro = documento.RootElement;

        Assert.True(cadastro.GetProperty("id").GetInt32() > 0);
        Assert.Equal(
            request.Nome,
            cadastro.GetProperty("nome").GetString());
        Assert.Equal(
            request.Email,
            cadastro.GetProperty("email").GetString());

        Assert.False(cadastro.TryGetProperty("senha", out _));
        Assert.False(cadastro.TryGetProperty("senhaHash", out _));
    }

    [Fact]
    public async Task Cadastrar_ComSenhaCurta_DeveRetornar400()
    {
        var request = new CadastroRequest
        {
            Nome = "Ana",
            Email = "ana@example.test",
            Senha = "Teste12"
        };

        using var resposta = await client.PostAsJsonAsync(
            "/api/auth/cadastro",
            request);

        Assert.Equal(HttpStatusCode.BadRequest, resposta.StatusCode);

        using var documento = await resposta.Content
            .ReadFromJsonAsync<JsonDocument>();

        Assert.NotNull(documento);

        var erros = documento.RootElement.GetProperty("errors");

        Assert.True(erros.TryGetProperty("Senha", out var errosSenha));
        Assert.Contains(
            "A senha deve conter no mínimo 8 caracteres.",
            errosSenha.EnumerateArray().Select(erro => erro.GetString()));
    }

    [Fact]
    public async Task Cadastrar_ComEmailInvalido_DeveRetornar400()
    {
        var request = new CadastroRequest
        {
            Nome = "Ana",
            Email = "email-invalido",
            Senha = "Teste123!"
        };

        using var resposta = await client.PostAsJsonAsync(
            "/api/auth/cadastro",
            request);

        Assert.Equal(HttpStatusCode.BadRequest, resposta.StatusCode);

        using var documento = await resposta.Content
            .ReadFromJsonAsync<JsonDocument>();

        Assert.NotNull(documento);

        var erros = documento.RootElement.GetProperty("errors");

        Assert.True(erros.TryGetProperty("Email", out var errosEmail));
        Assert.Contains(
            "Informe um e-mail válido.",
            errosEmail.EnumerateArray().Select(erro => erro.GetString()));
    }

    [Fact]
    public async Task Cadastrar_ComEmailJaCadastrado_DeveRetornar409()
    {
        var primeiroRequest = new CadastroRequest
        {
            Nome = "Ana",
            Email = "ana@example.test",
            Senha = "Teste123!"
        };

        using var primeiraResposta = await client.PostAsJsonAsync(
            "/api/auth/cadastro",
            primeiroRequest);

        Assert.Equal(HttpStatusCode.Created, primeiraResposta.StatusCode);

        var segundoRequest = new CadastroRequest
        {
            Nome = "Maria",
            Email = "ANA@example.test",
            Senha = "Outra123!"
        };

        using var segundaResposta = await client.PostAsJsonAsync(
            "/api/auth/cadastro",
            segundoRequest);

        Assert.Equal(HttpStatusCode.Conflict, segundaResposta.StatusCode);
    }
}