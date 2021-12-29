namespace Shop.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Shop.Models.Product;
    using Shop.Services.Category;
    using Shop.Services.Product;
    using Shop.Services.Seller;
    using Shop.Services.User;
    public class ProductController : Controller
    {
        private readonly IUserService userService;
        private readonly ISellerService sellerService;
        private readonly ICategoryService categoryService;
        private readonly IProductService productService;
        public ProductController(IUserService userService,
                                ISellerService sellerService,
                                ICategoryService categoryService,
                                IProductService productService)
        {
            this.userService = userService;
            this.sellerService = sellerService;
            this.categoryService = categoryService;
            this.productService = productService;
        }
        [Authorize]
        public IActionResult Add()
        {
            if (!sellerService.IsSeller(userService.GetUserId()) && !User.IsInRole(Constants.Administrator.AdministratorRoleName))
            {
                return RedirectToAction("Become", "Seller");
            }
            return View(new ProductFormModel
            {
                Categories = categoryService.GetCategories()
            });
        }
        [Authorize]
        [HttpPost]
        public IActionResult Add(ProductFormModel productInput)
        {
            if (!sellerService.IsSeller(userService.GetUserId()) && !User.IsInRole(Constants.Administrator.AdministratorRoleName))
            {
                return RedirectToAction("Become", "Seller");
            }
            if (!ModelState.IsValid)
            {
                return View(productInput);
            }
            string userId = userService.GetUserId();
            var isAdded = productService.Add(productInput, userId);
            if (!isAdded)
            {
                return BadRequest();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
