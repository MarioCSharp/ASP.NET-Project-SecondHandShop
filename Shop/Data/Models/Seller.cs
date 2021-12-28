namespace Shop.Data.Models
{
    using Shop.Models;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static Constants.Seller;
    public class Seller
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        [MaxLength(SellerCityMaxLength)]
        public string City { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
