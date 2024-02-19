using Application.Models;

namespace Application.Contracts;

public interface IAuthenticationService
{
    AuthenticationResponse Authenticate(AuthenticationRequest request);
}
