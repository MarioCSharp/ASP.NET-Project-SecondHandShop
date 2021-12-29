namespace Shop.Services.Product
{
    using Shop.Models.Product;
    public interface IProductService
    {
        bool Add(ProductFormModel productInput, string userId);
    }
}
