namespace WebWorkPlace.Database.Models;

public class App
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public ICollection<AppUserRole> UserRoles { get; set; }
}