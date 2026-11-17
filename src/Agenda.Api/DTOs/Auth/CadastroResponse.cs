namespace Agenda.Api.DTOs.Auth;

public class CadastroResponse
{
    public int Id {get; set;}
    public string Nome {get; set;} = string.Empty;
    public string Email {get; set;} = string.Empty;
}