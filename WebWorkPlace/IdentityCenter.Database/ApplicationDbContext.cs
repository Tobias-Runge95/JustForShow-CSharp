using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebWorkPlace.Database.Models;

namespace WebWorkPlace.Database;

public class ApplicationDbContext : IdentityDbContext<IdentityUser<Guid>, IdentityRole<Guid>, Guid>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<UserClaim> UserClaims { get; set; }
    public DbSet<RoleClaim> RoleClaims { get; set; }
    public DbSet<App> Apps { get; set; }
    public DbSet<AppUserRole> AppUserRoles { get; set; }
    

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.HasDefaultSchema("identity");
        
        builder.Entity<App>()
            .HasKey(x => x.Id);
        
        var aur = builder.Entity<AppUserRole>();
        aur.HasKey(ur => new { ur.UserId, ur.RoleId, ur.AppId });

        aur
            .HasOne(ur => ur.User)
            .WithMany()
            .HasForeignKey(ur => ur.UserId);

        aur
            .HasOne(ur => ur.Role)
            .WithMany()
            .HasForeignKey(ur => ur.RoleId);

        aur
            .HasOne(ur => ur.App)
            .WithMany(a => a.UserRoles)
            .HasForeignKey(ur => ur.AppId);
    }
}