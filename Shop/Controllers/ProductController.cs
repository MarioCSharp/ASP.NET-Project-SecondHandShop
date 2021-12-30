namespace Shop.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Shop.Data;
    using Shop.Models.Product;
    using Shop.Services.Category;
    using Shop.Services.Product;
    using Shop.Services.Seller;
    using Shop.Services.User;
    using System.Linq;

    public class ProductController : Controller
    {
        private readonly IUserService userService;
        private readonly ISellerService sellerService;
        private readonly ICategoryService categoryService;
        private readonly IProductService productService;
        private readonly ShopDbContext context;
        public ProductController(IUserService userService,
                                ISellerService sellerService,
                                ICategoryService categoryService,
                                IProductService productService,
                                ShopDbContext context)
        {
            this.userService = userService;
            this.sellerService = sellerService;
            this.categoryService = categoryService;
            this.productService = productService;
            this.context = context;
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
        public IActionResult All([FromQuery] AllProductsQueryModel query)
        {
            var productsQuery = context.Products.AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.Search))
            {
                productsQuery = context
                    .Products
                    .Where(x => (x.Name.ToLower()).Contains(query.Search));
            }
            var products = productsQuery
                    .OrderByDescending(x => x.Id)
                    .Select(x => new ProductListingViewModel
                    {
                        Name = x.Name,
                        Description = x.Description,
                        Price = x.Price,
                        ImageURL = x.ImageURL,
                        Category = context.Categories.FirstOrDefault(y => y.Id == x.CategoryId),
                        CreaterEmail = x.CreaterEmail,
                        CreatedOn = x.CreatedOn
                    })
                    .ToList();
            return View(new AllProductsQueryModel
            {
                Search = query.Search,
                Products = products
            });
        }
    }
}
