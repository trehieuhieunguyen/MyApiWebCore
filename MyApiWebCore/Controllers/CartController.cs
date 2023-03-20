using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyApiWebCore.Data;
using MyApiWebCore.Models;
using MyApiWebCore.Repositories.IRepository;
using System.Data;
using System.Security.Claims;

namespace MyApiWebCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = UserRoles.Admin)]
    public class CartController : ControllerBase
    {
        private ICartRepository cartRepository;
        UserManager<ApplicationUser> _userManager;

        public CartController(ICartRepository cartRepository,
            UserManager<ApplicationUser> _userManager)
        {
            this.cartRepository = cartRepository;
            this._userManager = _userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCartItem()
        {
            try
            {
                return Ok(await cartRepository.GetAllCartAsync());
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> AddCartAsync(CartModel cartModel)
        {
            
            try
            {
                if (User.Identity!.IsAuthenticated)
                {
                    // Get User
                    var user = await _userManager.GetUserAsync(User);

                    // Get Current UserId
                    var userId = user.Id;

                    var newCartModel = await cartRepository.AddCartAsync(userId, cartModel);
                    var cart = await cartRepository.GetCartByIdAsync(newCartModel);
                    return cart == null ? NotFound() : Ok(cart);
                }
                else
                {
                    return NotFound();
                }
                
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCartAsync(int id)
        {
            try
            {
                await cartRepository.DeleteCartAsync(id);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCartAsync(int id, CartModel cartModel)
        {
            try
            {
                await cartRepository.UpdateCartAsync(id, cartModel);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
