using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MyApiWebCore.Data;
using MyApiWebCore.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyApiWebCore.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private IConfiguration configuration;

        public AccountRepository(UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            RoleManager<IdentityRole> roleManager) 
        { 
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
            this.roleManager = roleManager;
        }
        public async Task<string> SignInAsync(SignInModel model)
        {
            var user = await userManager.FindByNameAsync(model.Email);
            var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
            if(!result.Succeeded)
            {
                return string.Empty;
            }
            var userRoles = await userManager.GetRolesAsync(user);
            List<Claim> authenClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, model.Email), 
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())    
            };
           
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
            if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await roleManager.RoleExistsAsync(UserRoles.User))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

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
