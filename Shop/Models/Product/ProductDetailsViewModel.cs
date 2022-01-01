namespace Shop.Models.Product
{
    public class ProductDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        public string CategoryName { get; set; }
        public decimal Price { get; set; }
    }
}
