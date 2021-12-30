namespace Shop.Models.Product
{
    using Data.Models;
    using System;
    public class ProductListingViewModel
    {
        public int Id { get; set; }
        public string Name { get; init; }
        public string Description { get; init; }
        public decimal Price { get; init; }
        public string ImageURL { get; init; }
        public Category Category { get; init; }
        public string CreaterEmail { get; init; }
        public DateTime CreatedOn { get; init; }
        public string UserId { get; set; }
    }
}
