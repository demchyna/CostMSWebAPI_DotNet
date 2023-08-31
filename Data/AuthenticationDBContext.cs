using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CostMSWebAPI.Data;

public class AuthenticationDbContext : IdentityDbContext<IdentityUser>
{
    public AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
