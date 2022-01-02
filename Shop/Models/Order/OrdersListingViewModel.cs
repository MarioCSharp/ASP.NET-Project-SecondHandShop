namespace Shop.Models.Order
{
    using Shop.Models.Product;
    using System;
    using System.Collections.Generic;
    public class OrdersListingViewModel
    {
        public int Count { get; set; }
        public decimal Price { get; set; }
        public IEnumerable<ProductListingViewModel> Products { get; set; }
    }
}
