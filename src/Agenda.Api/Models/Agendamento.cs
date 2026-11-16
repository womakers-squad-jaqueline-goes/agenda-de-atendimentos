namespace Agenda.Api.Models;

public class Agendamento
{
    public int Id {get; set;}
    public int ProfissionalId {get; set;}
    public int ClienteId {get; set;}
    public int ServicoId {get; set;}
    public DateTime Inicio {get; set;}
    public DateTime Fim {get; set;}
    public decimal PrecoAplicado {get; set;}
    public StatusAgendamento Status {get; set;}
}