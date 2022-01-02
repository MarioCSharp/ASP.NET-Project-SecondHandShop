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
                productInput.Categories = categoryService.GetCategories();
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
            var products = productService.GetProducts(productsQuery.ToList());
            return View(new AllProductsQueryModel
            {
                Search = query.Search,
                Products = products
            });
        }
        [Authorize]
        public IActionResult MyProducts([FromQuery] AllProductsQueryModel query)
        {
            if (!sellerService.IsSeller(userService.GetUserId()))
            {
                return RedirectToAction("Become", "Seller");
            }
            var productsQuery = context.Products.AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.Search))
            {
                productsQuery = context
                    .Products
                    .Where(x => (x.Name.ToLower()).Contains(query.Search.ToLower()));
            }
            var myProducts = productService.GetProducts(productsQuery.ToList())
                .Where(x => x.UserId == userService.GetUserId());
            return View(new AllProductsQueryModel
            {
                Search = query.Search,
                Products = myProducts
            });
        }
        [Authorize]
        public IActionResult Delete(int Id)
        {
            var product = productService.GetProduct(Id);
            if (product == null)
            {
                return BadRequest();
            }
            if (userService.GetUserId() != product.UserId)
            {
                return BadRequest();
            }
            var deleted = productService.Delete(product);
            if (!deleted)
            {
                return BadRequest();
            }
            return RedirectToAction(nameof(MyProducts));
        }
        [Authorize]
        public IActionResult Edit(int Id)
        {
            var product = productService.GetProduct(Id);
            if (product == null)
            {
                return BadRequest();
            }
            if (userService.GetUserId() != product.UserId)
            {
                return BadRequest();
            }
            return View(new ProductFormModel
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CategoryId = product.CategoryId,
                ImageURL = product.ImageURL,
                Categories = categoryService.GetCategories()
            });
        }
        [Authorize]
        [HttpPost]
        public IActionResult Edit(ProductFormModel toEdit, int Id)
        {
            if (!ModelState.IsValid)
            {
                return View(toEdit);
            }
            var edited = productService.Edit(toEdit, Id);
            if (!edited)
            {
                return BadRequest();
            }
            return RedirectToAction(nameof(MyProducts));
        }
        [Authorize]
        public IActionResult Details(int Id)
        {
            var product = productService.GetProduct(Id);
            return View(new ProductDetailsViewModel
            {
                Id = product.Id,
                Name= product.Name,
                ImageURL= product.ImageURL,
                Description = product.Description,
                Price= product.Price,
                CategoryName = categoryService.GetCategory(product.CategoryId).Name
            });
        }
    }
}
