using Microsoft.AspNetCore.Identity;
using MyApiWebCore.Models;

namespace MyApiWebCore.Repositories.IRepository
{
    public interface IAccountRepository
    {
        public Task<IdentityResult> SignUpAsync(SignUpModel model);
        public Task<object> SignInAsync(SignInModel model);
        public Task<string> RefreshAccessToken(string refreshToken);
    }
}
