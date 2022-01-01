namespace Shop.Services.Cart
{
    using Data.Models;
    using Shop.Models.Product;
    using System.Collections.Generic;
    public interface ICartService
    {
        bool Add(Product product, string userId);
        List<ProductListingViewModel> GetUserCart(string userId);
    }
}
