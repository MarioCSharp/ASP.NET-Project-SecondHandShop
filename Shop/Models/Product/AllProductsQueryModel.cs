namespace Shop.Models.Product
{
    using System.Collections.Generic;
    public class AllProductsQueryModel
    {
        public string Search { get; set; }
        public IEnumerable<ProductListingViewModel> Products { get; set; }
    }
}
