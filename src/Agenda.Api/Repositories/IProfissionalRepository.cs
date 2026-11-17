using Agenda.Api.Models;

namespace Agenda.Api.Repositories;

public interface IProfissionalRepository
{
    Task<Profissional?>BuscarPorEmailAsync(string email);
    Task AdicionarAsync(Profissional profissional);
    
}