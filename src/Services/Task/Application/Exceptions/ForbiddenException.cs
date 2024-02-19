namespace Application.Exceptions;

public class ForbiddenException : ApplicationException
{
    public ForbiddenException() : base("403 Forbidden Error - You don't have permission to access / on this server.")
    {

    }
}