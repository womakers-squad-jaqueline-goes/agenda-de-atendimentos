using System.ComponentModel.DataAnnotations;

namespace Agenda.Api.DTOs.Auth;

public class CadastroRequest
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    public string Nome {get; set;} = string.Empty;
    [Required(ErrorMessage = "O e-mail é obrigatório.")]
    [EmailAddress(ErrorMessage = "Informe um e-mail válido.")]
    public string Email {get; set;} = string.Empty;
    [Required(ErrorMessage = "A senha é obrigatória.")]
    [MinLength(8, ErrorMessage = "A senha deve conter no mínimo 8 caracteres.")]
    public string Senha {get; set;} = string.Empty;

}