using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyApiWebCore.Data;
using MyApiWebCore.Models;
using MyApiWebCore.Repositories;
using MyApiWebCore.Repositories.IRepository;
using System.Data;
using System.Security.Claims;

namespace MyApiWebCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionProduct : ControllerBase
    {
        private ITransactionHistoryRepositrory historyRepositrory;
        private UserManager<ApplicationUser> _userManager;

        public TransactionProduct(ITransactionHistoryRepositrory historyRepositrory,
            UserManager<ApplicationUser> userManager) 
        {
            this.historyRepositrory = historyRepositrory;
            this._userManager = userManager;
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> TransactionProductAsync(List<OrderItemModel> orderDetails)
        {
            if (orderDetails == null)
            {
                return NotFound();
            }
            if (User.Identity!.IsAuthenticated)
            {
                // Get User
                var user = await _userManager.GetUserAsync(User);

                // Get Current UserId 
                var userId = user.Id;

                var orders = await historyRepositrory.TransactionHistory(userId, orderDetails);
                return Ok(orders);
                
            }
            else
            {
                return NotFound();
            }
        }
    }
}
