namespace Shop.Models.Product
{
    using Shop.Models.Category;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Constants.Product;
    using static Constants.Models;
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
        [Display(Name = "Image Url")]
        [MinLength(ImageUrlMinLength)]
        public string ImageURL { get; set; }
        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public IEnumerable<CategoryListingViewModel> Categories { get; set; }
    }
}
