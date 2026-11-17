namespace Agenda.Api.Models;

public class Expediente
{
    public int Id {get; set;}
    public int ProfissionalId {get; set;}
    public DayOfWeek DiaDaSemana {get; set;}
    public TimeOnly Inicio {get; set;}
    public TimeOnly Fim {get; set;}
}