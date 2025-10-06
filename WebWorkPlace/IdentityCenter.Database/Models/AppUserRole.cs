namespace WebWorkPlace.Database.Models;

public class AppUserRole
{
    public Guid UserId { get; set; }
    public User User { get; set; }

    public Guid RoleId { get; set; }
    public Role Role { get; set; }

    public Guid AppId { get; set; }
    public App App { get; set; }
}