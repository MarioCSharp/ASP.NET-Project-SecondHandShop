namespace Shop.Data.Models
{
    using Shop.Models;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static Constants.Product;
    public class Product
    {
        [Key]
        public int Id { get; set; }
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
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }
        public User User { get; set; }
        [Required]
        public string CreaterEmail { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; }
        [Required]
        public bool IsInSomeoneCart { get; set; }
    }
}
