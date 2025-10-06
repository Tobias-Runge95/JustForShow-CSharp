namespace WebWorkPlace.Core.MediatR.Commands.Authentication;

public class LoginRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}