namespace Shop.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;
    using static Constants.User;
    public class User : IdentityUser
    {
        [MaxLength(MaxFullNameLength)]
        public string FullName { get; set; }
    }
}
