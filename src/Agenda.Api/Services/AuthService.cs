using Agenda.Api.Repositories;
using Agenda.Api.DTOs.Auth;
using Agenda.Api.Exceptions;
using Agenda.Api.Models;
using Microsoft.AspNetCore.Identity;

namespace Agenda.Api.Services;

public class AuthService
{
    private readonly IProfissionalRepository _profissionalRepository;
    private readonly IPasswordHasher<Profissional> _passwordHasher;

    public AuthService(IProfissionalRepository profissionalRepository, IPasswordHasher<Profissional> passwordHasher)
    {
        _profissionalRepository = profissionalRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<CadastroResponse> CadastrarAsync(CadastroRequest request)
    {
        var profissionalExistente = await _profissionalRepository.BuscarPorEmailAsync(request.Email);

        if (profissionalExistente is not null)
        {
            throw new ConflictException("Este e-mail já está cadastrado.");
        }

        var profissional = new Profissional
        {
            Nome = request.Nome.Trim(),
            Email = request.Email

        };

        profissional.SenhaHash = _passwordHasher.HashPassword
        (profissional, request.Senha);

        await _profissionalRepository.AdicionarAsync(profissional);

        return new CadastroResponse()
        {
            Id = profissional.Id,
            Nome = profissional.Nome,
            Email = profissional.Email
        };
    }
}