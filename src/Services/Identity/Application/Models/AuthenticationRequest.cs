namespace Application.Models;

public class AuthenticationRequest
{
    /// <example>admin</example>
    public required string UserName { get; set; }
    /// <example>P@$$w0rd</example>
    public required string Password { get; set; }
}