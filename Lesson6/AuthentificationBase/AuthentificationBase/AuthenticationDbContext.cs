using AuthentificationBase.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthentificationBase
{
    public class AuthenticationDbContext : IdentityDbContext<User>
    {
        public AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}