namespace Shop.Services.Product
{
    using Shop.Models.Product;
    using System.Collections.Generic;
    using Data.Models;
    public interface IProductService
    {
        bool Add(ProductFormModel productInput, string userId);
        List<ProductListingViewModel> GetProducts(List<Product> productsQuery);
    }
}
