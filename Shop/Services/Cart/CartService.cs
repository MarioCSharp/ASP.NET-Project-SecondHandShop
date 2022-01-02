namespace Shop.Services.Cart
{
    using Data.Models;
    using Shop.Data;
    using Shop.Models.Product;
    using System.Collections.Generic;
    using System.Linq;
    public class CartService : ICartService
    {
        private readonly ShopDbContext context;
        public CartService(ShopDbContext context)
        {
            this.context = context;
        }
        public bool Add(Product product, string userId)
        {
            if (product == null)
            {
                return false;
            }
            if (context.Carts.Any(x => x.ProductId == product.Id))
            {
                return false;
            }
            Cart cart = new Cart
            {
                UserId = userId,
                ProductId = product.Id,
            };
            product.IsInSomeoneCart = true;
            context.Carts.Add(cart);
            context.SaveChanges();
            return true;
        }
        public List<ProductListingViewModel> GetUserCart(string userId)
        {
            var userCarts = context.Carts.Where(x => x.UserId == userId).ToList();
            List<Product> products = new List<Product>();
            foreach (var cart in userCarts)
            {
                products.Add(context.Products.FirstOrDefault(x => x.Id == cart.ProductId));
            }
            return products
                .OrderByDescending(x => x.Id)
                .Select(x => new ProductListingViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    ImageURL = x.ImageURL,
                    CreaterEmail = x.CreaterEmail
                })
                .ToList();
        }
        public bool Delete(int Id, string userId)
        {
            var product = context.Products.Find(Id);
            if (product == null)
            {
                return false;
            }
            var cart = context.Carts.FirstOrDefault(x => x.ProductId == product.Id);
            if (cart.UserId != userId)
            {
                return false;
            }
            context.Carts.Remove(cart);
            product.IsInSomeoneCart = false;
            context.SaveChanges();
            return true;
        }
    }
}
