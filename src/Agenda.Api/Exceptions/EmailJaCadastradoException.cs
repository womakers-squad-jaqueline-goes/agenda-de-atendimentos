namespace Agenda.Api.Exceptions;

public class EmailJaCadastradoException : Exception
{
    public EmailJaCadastradoException(string mensagem)
        : base(mensagem)
    {
    }
}