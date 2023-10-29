using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MyWebApplication.Entities;
using MyWebApplication.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyWebApplication.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthenticationService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task<(int, string)> Login(LoginModel model)
        {
            ApplicationUser? user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                return (0, "Invalid UserName");
            }

            if (!await _userManager.CheckPasswordAsync(user, model.Password))
            {
                return (0, "Invalid password");
            }

            IList<string> userRoles = await _userManager.GetRolesAsync(user);
            List<Claim> authClaims = new()
            {
               new Claim(ClaimTypes.Name, user.UserName!),
               new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (string userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }
            string token = GenerateToken(authClaims);
            return (1, token);
        }

        public async Task<(int, string)> Registration(RegistrationModel model, string role)
        {
            ApplicationUser? userExists = await _userManager.FindByNameAsync(model.UserName);
            if (userExists != null)
            {
                return (0, "User already exists");
            }

            ApplicationUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.UserName,
                FirstName = model.FirstName,
                LastName = model.LastName,
            };
            IdentityResult createUserResult = await _userManager.CreateAsync(user, model.Password);
            if (!createUserResult.Succeeded)
            {
                return (0, "User creation failed! Please check user details and try again.");
            }

            if (!await _roleManager.RoleExistsAsync(role))
            {
                _ = await _roleManager.CreateAsync(new IdentityRole(role));
            }

            if (await _roleManager.RoleExistsAsync(role))
            {
                _ = await _userManager.AddToRoleAsync(user, role);
            }

            return (1, "User created successfully!");
        }

        private string GenerateToken(IEnumerable<Claim> claims)
        {
            JwtSecurityTokenHandler tokenHandler = new();
            SecurityToken token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _configuration["JWTKey:ValidIssuer"],
                Audience = _configuration["JWTKey:ValidAudience"],
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTKey:Secret"]!)), SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(claims)
            });
            return tokenHandler.WriteToken(token);
        }
    }
}
