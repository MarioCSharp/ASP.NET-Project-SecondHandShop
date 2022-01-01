namespace Shop.Models.Cart
{
    using Shop.Models.Product;
    using System.Collections.Generic;
    public class CartViewModel
    {
        public IEnumerable<ProductListingViewModel> Products { get; set; }
    }
}
