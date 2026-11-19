namespace Agenda.Api.Exceptions;

public class ConflictException : Exception
{
    public ConflictException(string mensagem) : base(mensagem)
    {
    }
}