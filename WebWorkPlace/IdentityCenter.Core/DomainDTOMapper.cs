using System.Security.Claims;
using WebWorkPlace.Database.Models;

namespace WebWorkPlace.Core;

public static class DomainDTOMapper
{
    public static List<Claim> MapToClaim(this List<AppUserRole> roles)
    {
        return roles.Select(x => new Claim(ClaimTypes.Role, x.Role.Name)).ToList();
    }
}