namespace Shop.Services.Product
{
    using Shop.Data;
    using Shop.Data.Models;
    using Shop.Models.Product;
    using System;
    using System.Linq;
    public class ProductService : IProductService
    {
        private readonly ShopDbContext context;
        public ProductService(ShopDbContext context)
        {
            this.context = context;
        }
        public bool Add(ProductFormModel productInput, string userId)
        {
            if (userId == null || productInput.Name == null || productInput.Description == null || productInput.Price <= 0
                || productInput.ImageURL == null)
            {
                return false;
            }
            var product = new Product
            {
                Name = productInput.Name,
                Description = productInput.Description,
                Price = productInput.Price,
                ImageURL = productInput.ImageURL,
                CategoryId = productInput.CategoryId,
                UserId = userId,
                CreaterEmail = context.Users.FirstOrDefault(p => p.Id == userId).Email,
                CreatedOn = DateTime.Now
            };
            context.Products.Add(product);
            context.SaveChanges();
            return true;
        }
    }
}
