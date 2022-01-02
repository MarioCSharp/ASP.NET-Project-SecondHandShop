namespace Shop.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Shop.Models.Cart;
    using Shop.Services.Cart;
    using Shop.Services.Product;
    using Shop.Services.User;

    public class CartController : Controller
    {
        private readonly IProductService productService;
        private readonly IUserService userService;
        private readonly ICartService cartService;
        public CartController(IProductService productService,
                              IUserService userService,
                              ICartService cartService)
        {
            this.productService = productService;
            this.userService = userService;
            this.cartService = cartService;
        }
        [Authorize]
        public IActionResult Add(int Id)
        {
            var product = productService.GetProduct(Id);
            var userId = userService.GetUserId();
            var added = cartService.Add(product, userId);
            if (!added)
            {
                return BadRequest();
            }
            return RedirectToAction(nameof(Cart));
        }
        [Authorize]
        public IActionResult Cart()
        {
            return View(new CartViewModel
            {
                Products = cartService.GetUserCart(userService.GetUserId())
            });
        }
        [Authorize]
        public IActionResult Remove(int Id)
        {
            var deleted = cartService.Delete(Id, userService.GetUserId());
            if (!deleted)
            {
                return NotFound();
            }
            return RedirectToAction("All", "Product");
        }
    }
}
