namespace Shop.Models.Product
{
    using Shop.Models.Category;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Constants.Product;
    public class ProductFormModel
    {
        [Required]
        [MaxLength(ProductNameMaxLength)]
        public string Name { get; set; }
        [Required]
        [MaxLength(ProductDescriptionMaxLength)]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string ImageURL { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public IEnumerable<CategoryListingViewModel> Categories { get; set; }
    }
}
