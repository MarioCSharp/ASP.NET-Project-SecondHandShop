namespace Shop.Services.Seller
{
    using Shop.Models.Seller;
    public interface ISellerService
    {
        bool Create(BecomeSellerFormModel becomeInput, string userId);
        bool IsSeller(string userId);
    }
}
