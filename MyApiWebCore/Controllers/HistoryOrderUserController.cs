using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyApiWebCore.Data;
using MyApiWebCore.Repositories.IRepository;

namespace MyApiWebCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = UserRoles.Admin)]
    public class HistoryOrderUserController : ControllerBase
    {
        private IHistoryOrderOfUserRepository historyOrderOfUser;
        private UserManager<ApplicationUser> userManager;

        public HistoryOrderUserController(IHistoryOrderOfUserRepository historyOrderOfUser,
            UserManager<ApplicationUser> userManager) {
            this.historyOrderOfUser = historyOrderOfUser;
            this.userManager = userManager;
        }
        [HttpGet("~/GetLastestOrderUser")]
        public async Task<IActionResult> HistoryLastOrderUser()
        {
            try
            {
                if (User.Identity!.IsAuthenticated)
                {
                    var user = await userManager.GetUserAsync(User);
                    var orderDetail = await historyOrderOfUser.HistoryLatestOrder(user.Id);
                    return orderDetail == null ? NotFound() : Ok(orderDetail);
                }

                return BadRequest();
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("~/GetOrdersUser")]
        public async Task<IActionResult> HistoryOrdersUser()
        {
            try
            {
                if (User.Identity!.IsAuthenticated)
                {
                    var user = await userManager.GetUserAsync(User);
                    var orderDetail = await historyOrderOfUser.HistoryOrdersOfUser(user.Id);
                    return orderDetail == null ? NotFound() : Ok(orderDetail);
                }

                return BadRequest();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
