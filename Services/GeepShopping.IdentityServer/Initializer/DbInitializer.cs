using System.Security.Claims;
using GeepShopping.IdentityServer.Configuration;
using GeepShopping.IdentityServer.Model;
using GeepShopping.IdentityServer.Model.Context;
using IdentityModel;
using Microsoft.AspNetCore.Identity;

namespace GeepShopping.IdentityServer.Initializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly MySQLContext _context;
        private readonly UserManager<ApplicationUser> _user;
        private readonly RoleManager<IdentityRole> _role;

        public DbInitializer(MySQLContext context, UserManager<ApplicationUser> user, RoleManager<IdentityRole> role)
        {
            _context = context;
            _user = user;
            _role = role;
        }        

        public void Initialize()
        {
            // Verificar e criar roles
            if (_role.FindByNameAsync(IdentityConfiguration.Admin).Result == null)
            {
                _role.CreateAsync(new IdentityRole(IdentityConfiguration.Admin)).GetAwaiter().GetResult();
            }

            if (_role.FindByNameAsync(IdentityConfiguration.Client).Result == null)
            {
                _role.CreateAsync(new IdentityRole(IdentityConfiguration.Client)).GetAwaiter().GetResult();
            }

            // Criar usuário Admin
            if (_user.FindByEmailAsync("junior-admin@gmail.com.br").Result == null)
            {
                ApplicationUser admin = new ApplicationUser()
                {
                    UserName = "junior-admin",
                    Email = "junior-admin@gmail.com.br",
                    EmailConfirmed = true,
                    PhoneNumber = "+55 (71) 99845-6898",
                    FirstName = "Junior",
                    LastName = "Admin"
                };

                _user.CreateAsync(admin, "Admin123@").GetAwaiter().GetResult();
                _user.AddToRoleAsync(admin, IdentityConfiguration.Admin).GetAwaiter().GetResult();
                _user.AddClaimsAsync(admin, new Claim[]
                {
                    new Claim(JwtClaimTypes.Name, $"{admin.FirstName} {admin.LastName}"),
                    new Claim(JwtClaimTypes.GivenName, admin.FirstName),
                    new Claim(JwtClaimTypes.FamilyName, admin.LastName),
                    new Claim(JwtClaimTypes.Role, IdentityConfiguration.Admin),
                }).GetAwaiter().GetResult();
            }

            // Criar usuário Client
            if (_user.FindByEmailAsync("junior-client@gmail.com.br").Result == null)
            {
                ApplicationUser client = new ApplicationUser()
                {
                    UserName = "junior-client",
                    Email = "junior-client@gmail.com.br",
                    EmailConfirmed = true,
                    PhoneNumber = "+55 (71) 99845-6898",
                    FirstName = "Junior",
                    LastName = "Client"
                };

                _user.CreateAsync(client, "Client123@").GetAwaiter().GetResult();
                _user.AddToRoleAsync(client, IdentityConfiguration.Client).GetAwaiter().GetResult();
                _user.AddClaimsAsync(client, new Claim[]
                {
                    new Claim(JwtClaimTypes.Name, $"{client.FirstName} {client.LastName}"),
                    new Claim(JwtClaimTypes.GivenName, client.FirstName),
                    new Claim(JwtClaimTypes.FamilyName, client.LastName),
                    new Claim(JwtClaimTypes.Role, IdentityConfiguration.Client),
                }).GetAwaiter().GetResult();
            }
        }

    }
}