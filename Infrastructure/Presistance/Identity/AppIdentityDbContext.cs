using Domain.Entities.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Persistance.Identity;
public class AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) 
    : IdentityDbContext<User>(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<User>().ToTable("Users");
        builder.Entity<IdentityRole>().ToTable("Roles");
        builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
        builder.Ignore<IdentityUserClaim<string>>();
        builder.Ignore<IdentityRoleClaim<string>>();
        builder.Ignore<IdentityUserLogin<string>>();
        builder.Ignore<IdentityUserToken<string>>();
        builder.Entity<User>().OwnsOne(u => u.Address, a =>
        {
            a.WithOwner();
            a.Property(x => x.Street).HasColumnName("Street");
            a.Property(x => x.City).HasColumnName("City");
            a.Property(x => x.Country).HasColumnName("Country");
        });
    }
}
