namespace WebWorkPlace.Core.MediatR.Commands.Authentication;

public class AppLogin
{
    public Guid UserId { get; set; }
    public string AppName { get; set; }
}