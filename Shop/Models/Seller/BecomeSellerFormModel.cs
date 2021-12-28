namespace Shop.Models.Seller
{
    using System.ComponentModel.DataAnnotations;
    using static Constants.Seller;
    public class BecomeSellerFormModel
    {
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        [MaxLength(SellerCityMaxLength)]
        public string City { get; set; }
    }
}
