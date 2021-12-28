namespace Shop.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static Constants.Category;
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(CategoryNameMaxLength)]
        public string Name { get; set; }
    }
}
