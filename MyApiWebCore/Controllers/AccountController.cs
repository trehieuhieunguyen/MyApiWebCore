using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyApiWebCore.Models;
using MyApiWebCore.Repositories.IRepository;

namespace MyApiWebCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IAccountRepository accountRepo;

        public AccountController(IAccountRepository accountRepository) 
        { 
            accountRepo = accountRepository;
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(SignUpModel model)
        {
            var result =await accountRepo.SignUpAsync(model);
            if (result.Succeeded)
            {
                return Ok(result.Succeeded);
            }
            else
            {
                return Unauthorized();
            }
        }
        
        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(SignInModel model)
        {
            var result = await accountRepo.SignInAsync(model);
            if (string.IsNullOrEmpty(result))
            {
                return Unauthorized();
            }
            else
            {
                return Ok(result);
            }
        }
        
    }
}
