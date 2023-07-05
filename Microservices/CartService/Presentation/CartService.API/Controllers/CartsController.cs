using CartService.API.IntegrationEvents.Events;
using CartService.Application.Abstractions;

using CartService.Domain.Entities;
using EventBus.Base.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Net.Http;
using System.Security.Claims;
using System.Text;

namespace CartService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ICartService _cartService;
        private readonly IEventBus _eventBus;

        public CartsController(IHttpContextAccessor contextAccessor, ICartService cartService, IEventBus eventBus)
        {
            _contextAccessor = contextAccessor;
            _cartService = cartService;
            _eventBus = eventBus;
        }

        [HttpGet("{userId}")]
        [Authorize]
        public async Task<IActionResult> GetCartByIdAsync(string userId)
        {
            var cart = await _cartService.GetCartAsync(userId: userId);

            return Ok(cart ?? new Cart(userId: userId));

        }
        [HttpPost("update")]
        [Authorize]
        public async Task<IActionResult> UpdateCartAsync([FromBody] Cart cart)
        {
            return Ok(await _cartService.UpdateCartAsync(cart));
        }
        [HttpPost("additem")]
        [Authorize]
        public async Task<IActionResult> AddItemToCartAsync([FromBody] CartItem cartItem)
        {
            string? userId = null;
            Claim? claim = _contextAccessor.HttpContext?.User.FindFirst("UserId");
            if (claim != null)
                userId = claim.ToString().Replace("UserId: ", "").Replace(" ", "");

            var cart = await _cartService.GetCartAsync(userId: userId);
            if (cart is null)
                cart = new Cart(userId: userId);

            cart.Items.Add(cartItem);

            await _cartService.UpdateCartAsync(cart);

            return Ok();

        }
        [HttpPost("checkout")]
        [Authorize]
        public async Task<IActionResult> CheckoutAsync([FromBody] CartCheckout cartCheckout)
        {
            var cart = await _cartService.GetCartAsync(cartCheckout.UserId);
            if (cart is null)
                return BadRequest("Cart is not found!!");


            var eventMessage = new OrderCreatedIntegrationEvent(userId: cartCheckout.UserId, city: cartCheckout.City, street: cartCheckout.Street, state: cartCheckout.State, country: cartCheckout.Country
                , cardNumber: cartCheckout.CardNumber, cardHolderName: cartCheckout.CardHolderName, cart: cart);

            try
            {
                _eventBus.Publish(eventMessage);
            }
            catch (Exception ex)
            {
                //zamanım olsaydı log eklerdim :)
                throw;
            }

            return Ok();

        }
        [HttpDelete("{userId}")]
        [Authorize]
        public async Task<IActionResult> DeleteAsync(string userId)
        {
            bool result = await _cartService.DeleteCartAsync(userId);
            return Ok(result);

        }

    }
}
