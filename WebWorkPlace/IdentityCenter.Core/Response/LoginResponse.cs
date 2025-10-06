namespace WebWorkPlace.Core.Response;

public record class LoginResponse
{
    public string AuthToken { get; init; }
    public string RenewToken { get; init; }
    public Guid UserId { get; init; }
}