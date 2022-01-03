namespace Shop.Services.Order
{
    using Shop.Data;
    using System.Linq;
    using Data.Models;
    using System;
    using System.Collections.Generic;

    public class OrderService : IOrderService
    {
        private readonly ShopDbContext context;
        public OrderService(ShopDbContext context)
        {
            this.context = context;
        }
        public bool ConfirmOrder(string userId)
        {
            var userCart = context.Carts.Where(x => x.UserId == userId);
            var count = userCart.Count();
            if (count <= 0)
            {
                return false;
            }
            var userProducts = new List<Product>();
            foreach (var product in userCart)
            {
                var productU = context.Products.Where(x => x.Id == product.ProductId).FirstOrDefault();
                userProducts.Add(productU);
                context.Products.Remove(productU);
            }
            Order order = new Order
            {
                IssuedOn = DateTime.Now,
                RecipientId = userId,
                Products = string.Join(", ", userProducts.Select(x => x.Name)),
                ProductsCount = count
            };
            context.Orders.Add(order);
            context.SaveChanges();
            return true;
        }
    }
}
