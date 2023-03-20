using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyApiWebCore.Data;
using MyApiWebCore.Models;
using MyApiWebCore.Repositories.IRepository;
using NuGet.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;

namespace MyApiWebCore.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ProductStoreContext context;
        private IConfiguration configuration;

        public AccountRepository(UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            RoleManager<IdentityRole> roleManager,
            ProductStoreContext context) 
        { 
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
            this.roleManager = roleManager;
            this.context = context;
        }
        public async Task<object> SignInAsync(SignInModel model)
        {
            var user = await userManager.FindByNameAsync(model.Email);
            var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
            if(!result.Succeeded)
            {
                return string.Empty;
            }
            var tokenAccess = await GenerateJwtToken(user);
            var refreshToken = await GenerateRefreshTokenAsync(user.Id);

            //SetRefreshToken(refreshToken);
            return new { tokenAccess, refreshToken };
        }
        private async Task<string> GenerateRefreshTokenAsync(string userId)
        {
            var randomNumber = new byte[32];
            string newRefresherToken = null;
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                newRefresherToken = Convert.ToBase64String(randomNumber);
            }
            var refreshToken = new RefreshToken
            {
                Token = newRefresherToken,
                UserId = userId,
                ExpiresUtc = DateTime.UtcNow.AddDays(7) // thời gian sống của refresh token là 7 ngày
            };
            context.RefreshToken.Add(refreshToken);
            await context.SaveChangesAsync();
            return newRefresherToken;
        }
        public async Task<string> RefreshAccessToken(string refreshToken)
        {
            var existingToken = await context.RefreshToken.Include(rt => rt.User).SingleOrDefaultAsync(rt => rt.Token == refreshToken);
            if (existingToken == null || existingToken.ExpiresUtc < DateTime.UtcNow)
            {
                return null;
            }
            var user = existingToken.User;
            return await GenerateRefreshTokenAsync(user.Id);
        }

        private async Task<string> GenerateJwtToken(ApplicationUser user)
        {
            List<Claim> authenClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var userRoles = await userManager.GetRolesAsync(user);
            foreach (var userRole in userRoles)
            {
                authenClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }
            var token = GetToken(authenClaims);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]!));

            var token = new JwtSecurityToken(
                issuer: configuration["JWT:ValidIssuer"],
                audience: configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
        public async Task<IdentityResult> SignUpAsync(SignUpModel model)
        {
            var newUser = new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email
                
            };
            var result = await userManager.CreateAsync(newUser, model.Password);
            //Create Role for IdentityRole in db
            if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await roleManager.RoleExistsAsync(UserRoles.User))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.User));
            //Create Role for User
            if (await roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await userManager.AddToRoleAsync(newUser, UserRoles.Admin);
            }
            if (await roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await userManager.AddToRoleAsync(newUser, UserRoles.User);
            }
            return result;
        }
    }
}
