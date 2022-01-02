namespace Shop.Services.Product
{
    using Shop.Data;
    using Shop.Data.Models;
    using Shop.Models.Product;
    using System;
    using System.Collections.Generic;
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
        public List<ProductListingViewModel> GetProducts(List<Product> productsQuery)
        => productsQuery
            .OrderByDescending(x => x.Id)
            .Where(x => x.IsInSomeoneCart == false)
                    .Select(x => new ProductListingViewModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description,
                        Price = x.Price,
                        ImageURL = x.ImageURL,
                        Category = context.Categories.FirstOrDefault(y => y.Id == x.CategoryId),
                        CreaterEmail = x.CreaterEmail,
                        CreatedOn = x.CreatedOn,
                        UserId = x.UserId
                    })
                    .ToList();
        public Product GetProduct(int Id)
        => context.Products.Find(Id);
        public bool Delete(Product productInput)
        {
            if (productInput.UserId == null || productInput.Name == null || productInput.Description == null || productInput.Price <= 0
                || productInput.ImageURL == null)
            {
                return false;
            }
            context.Products.Remove(productInput);
            context.SaveChanges();
            return true;
        }
        public bool Edit(ProductFormModel productInput, int Id)
        {
            if (productInput.Name == null || productInput.Description == null || productInput.Price <= 0
                || productInput.ImageURL == null)
            {
                return false;
            }
            var product = GetProduct(Id);
            product.Name = productInput.Name;
            product.Description = productInput.Description;
            product.Price = productInput.Price;
            product.CategoryId = productInput.CategoryId;
            product.ImageURL = productInput.ImageURL;
            context.SaveChanges();
            return true;
        }
    }
}
