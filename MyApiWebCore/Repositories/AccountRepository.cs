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
        private IConfiguration configuration;

        public AccountRepository(UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration) 
        { 
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
        }
        public async Task<string> SignInAsync(SignInModel model)
        {
            var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
            if(!result.Succeeded)
            {
                return string.Empty;
            }
            var authenClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, model.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            var authenKey =new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration[key:"JWT:Secret"])) ;

            var toKen = new JwtSecurityToken(
                issuer: configuration["JWT:ValidIssuer"],
                audience: configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(10),
                claims: authenClaims,
                signingCredentials: new SigningCredentials(authenKey,SecurityAlgorithms.HmacSha512Signature)
                );
            return new JwtSecurityTokenHandler().WriteToken(toKen);
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
            return await userManager.CreateAsync(newUser, model.Password);
        }
    }
}
