namespace Shop.Services.Statistics
{
    using Shop.Data;
    using System.Linq;

    public class StatisticsService : IStatisticsService
    {
        private readonly ShopDbContext context;
        public StatisticsService(ShopDbContext context)
        => this.context = context;
        public StatisticsServiceModel GetStatistics()
        {
            var totalProducts = context.Products.Count();
            var totalUsers = context.Users.Count();
            var totalOrders = context.Orders.Count();
            var totalSellers = context.Sellers.Count();
            return new StatisticsServiceModel
            {
                TotalProducts = totalProducts,
                TotalUsers = totalUsers,
                TotalPurchases = totalOrders,
                TotalSellers = totalSellers
            };
        }
    }
}
