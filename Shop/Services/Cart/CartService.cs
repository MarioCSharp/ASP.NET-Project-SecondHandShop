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
            if (userId == null || product.Name == null || product.Description == null || product.Price <= 0
                || product.ImageURL == null)
            {
                return false;
            }
            Cart cart = new Cart
            {
                UserId = userId,
                ProductId = product.Id,
            };
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
    }
}
