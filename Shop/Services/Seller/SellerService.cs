namespace Shop.Services.Seller
{
    using Data.Models;
    using Shop.Data;

    public class SellerService : ISellerService
    {
        private readonly ShopDbContext context;
        public SellerService(ShopDbContext context)
        {
            this.context = context;
        }
        public bool Create(Seller seller)
        {
            if (seller.PhoneNumber == null || seller.City == null)
            {
                return false;
            }
            context.Sellers.Add(seller);
            context.SaveChanges();
            return true;
        }
    }
}
