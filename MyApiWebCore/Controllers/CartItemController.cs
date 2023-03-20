using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyApiWebCore.Data;
using MyApiWebCore.Models;
using MyApiWebCore.Repositories.IRepository;
using System.Data;

namespace MyApiWebCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = UserRoles.Admin)]
    public class CartItemController : ControllerBase
    {
        private ICartItemRepository cartItemRepository;

        public CartItemController(ICartItemRepository cartItemRepository)
        {
            this.cartItemRepository = cartItemRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCartItem()
        {
            try
            {
                return Ok(await cartItemRepository.GetAllCartDetailAsync());
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddCartItem(CartItemModel cartItemModel)
        {
            try
            {
                var newCartItem = await cartItemRepository.AddCartItemsAsyn(cartItemModel);
                var cardsItem = await cartItemRepository.GetCartDetailAsyn(newCartItem);
                return cardsItem == null ? NotFound() : Ok(cardsItem);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCardItem(int id, CartItemModel cartItemModel)
        {
            try
            {
                await cartItemRepository.UpdateCartDetailAsyn(id, cartItemModel);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCartItem(int id)
        {
            try
            {
                var cardItemModel = await cartItemRepository.GetCartDetailAsyn(id);
                return Ok(cardItemModel);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
