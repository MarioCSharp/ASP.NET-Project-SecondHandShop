namespace Shop.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Shop.Models.Order;
    using Shop.Services.Cart;
    using Shop.Services.User;
    using System.Linq;

    public class OrderController : Controller
    {
        private readonly ICartService cartService;
        private readonly IUserService userService;
        public OrderController(ICartService cartService,
                               IUserService userService)
        {
            this.cartService = cartService;
            this.userService = userService;
        }
        [Authorize]
        public IActionResult Checkout()
        {
            var userCart = cartService.GetUserCart(userService.GetUserId());
            return View(new OrdersListingViewModel
            {
                Products = userCart,
                Count = userCart.Count,
                Price = userCart.Sum(x => x.Price)
            });
        }
        [Authorize]
        [HttpPost]
        public IActionResult Checkout(OrdersListingViewModel lst)
        {
            return View();
        }
    }
}
