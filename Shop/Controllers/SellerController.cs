namespace Shop.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Shop.Data.Models;
    using Shop.Models.Seller;
    using Shop.Services.Seller;
    using Shop.Services.User;

    public class SellerController : Controller
    {
        private readonly IUserService userService;
        private readonly ISellerService sellerService;
        public SellerController(IUserService userService,
                                ISellerService sellerService)
        {
            this.userService = userService;
            this.sellerService = sellerService;
        }
        public IActionResult Error() => View();
        [Authorize]
        public IActionResult Become()
        {
            if (sellerService.IsSeller(userService.GetUserId()))
            {
                return View(nameof(Error));
            }
            return View();
        }
        [Authorize]
        [HttpPost]
        public IActionResult Become(BecomeSellerFormModel becomeInput)
        {
            if (!ModelState.IsValid)
            {
                return View(becomeInput);
            }
            var userId = userService.GetUserId();
            sellerService.Create(becomeInput, userId);
            return RedirectToAction("Index", "Home");
        }
    }
}
