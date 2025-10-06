namespace WebWorkPlace.Core.MediatR.Commands.Authentication;

public class RenewTokenRequest
{
    public Guid UserId { get; set; }
    public string RenewToken { get; set; }
}