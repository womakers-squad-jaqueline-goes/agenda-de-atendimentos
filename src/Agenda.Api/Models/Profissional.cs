namespace Agenda.Api.Models;

public class Profissional
{
    private string email = string.Empty;
    public int Id {get; set;}
    public string Nome {get; set;} = string.Empty;
    public string Email {get => email; set => email = value.Trim().ToLowerInvariant();}
    public string SenhaHash {get; set;} = string.Empty;
}