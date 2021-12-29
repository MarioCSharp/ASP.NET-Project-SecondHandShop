namespace Shop.Services.Seller
{
    using Data.Models;
    using Shop.Data;
    using Shop.Models.Seller;
    using System.Linq;
    public class SellerService : ISellerService
    {
        private readonly ShopDbContext context;
        public SellerService(ShopDbContext context)
        {
            this.context = context;
        }
        public bool Create(BecomeSellerFormModel becomeInput, string userId)
        {
            Seller seller = new Seller
            {
                City = becomeInput.City,
                PhoneNumber = becomeInput.PhoneNumber,
                UserId = userId
            };
            if (seller.PhoneNumber == null || seller.City == null)
            {
                return false;
            }
            context.Sellers.Add(seller);
            context.SaveChanges();
            return true;
        }
        public bool IsSeller(string userId)
        => context
            .Sellers
            .Any(x => x.UserId == userId);
    }
}
