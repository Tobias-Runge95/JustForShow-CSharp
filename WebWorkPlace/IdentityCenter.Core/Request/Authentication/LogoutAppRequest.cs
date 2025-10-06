namespace WebWorkPlace.Core.MediatR.Commands.Authentication;

public class LogoutAppRequest
{
    public Guid UserId { get; set; }
    public string AppName { get; set; }
}