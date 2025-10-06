using WebWorkPlace.Database.Models;

namespace WebWorkPlace.Core.DTO;

public class AppWithRolesDto
{
    public string AppName { get; set; } = default!;
    public List<Role> Roles { get; set; } = new();
}