namespace Shop.Data.Models
{
    using Shop.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class Order
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime IssuedOn { get; set; }
        [ForeignKey("User")]
        public string RecipientId { get; set; }
        public User User { get; set; }
        [Required]
        public string Products { get; set; }
        [Required]
        public int ProductsCount { get; set; }
    }
}
