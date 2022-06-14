using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MessengerNetSix.Models
{
    public class User : IdentityUser
    {
        [Required]
        public int Phone { get; set; }
    }
}