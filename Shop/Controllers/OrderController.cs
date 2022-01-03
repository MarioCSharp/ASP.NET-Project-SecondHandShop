namespace Shop.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Shop.Models.Order;
    using Shop.Services.Cart;
    using Shop.Services.Order;
    using Shop.Services.User;
    using System.Linq;

    public class OrderController : Controller
    {
        private readonly ICartService cartService;
        private readonly IUserService userService;
        private readonly IOrderService orderService;
        public OrderController(ICartService cartService,
                               IUserService userService,
                               IOrderService orderService)
        {
            this.cartService = cartService;
            this.userService = userService;
            this.orderService = orderService;
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
        public IActionResult Buy()
        {
            var isDone = orderService.ConfirmOrder(userService.GetUserId());
            if (!isDone)
            {
                return BadRequest();
            }
            return RedirectToAction(nameof(Done));
        }
        [Authorize]
        public IActionResult Done()
        {
            return View();
        }
    }
}
