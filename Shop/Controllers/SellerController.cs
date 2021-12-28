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
        [Authorize]
        public IActionResult Become()
        {
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
            Seller seller = new Seller
            {
                City = becomeInput.City,
                PhoneNumber = becomeInput.PhoneNumber,
                UserId = userService.GetUserId()
            };
            sellerService.Create(seller);
            return RedirectToAction("Index", "Home");
        }
    }
}
