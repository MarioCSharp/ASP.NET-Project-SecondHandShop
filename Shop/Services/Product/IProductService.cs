namespace Shop.Services.Product
{
    using Shop.Models.Product;
    using System.Collections.Generic;
    using Data.Models;
    public interface IProductService
    {
        bool Add(ProductFormModel productInput, string userId);
        List<ProductListingViewModel> GetProducts(List<Product> productsQuery);
        Product GetProduct(int Id);
        bool Delete(Product product);
        bool Edit(ProductFormModel toEdit, int Id);
    }
}
