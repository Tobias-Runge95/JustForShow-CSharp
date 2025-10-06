namespace WebWorkPlace.Core.MediatR.Commands.Authentication;

public class RenewAppTokenRequest
{
    public string RenewToken { get; set; }
    public string AppName { get; set; }
    public Guid UserId { get; set; }
}