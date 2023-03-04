using Microsoft.AspNetCore.Identity;
using MyApiWebCore.Models;

namespace MyApiWebCore.Repositories
{
    public interface IAccountRepository
    {
        public Task<IdentityResult> SignUpAsync(SignUpModel model);
        public Task<string> SignInAsync(SignInModel model);
    }
}
