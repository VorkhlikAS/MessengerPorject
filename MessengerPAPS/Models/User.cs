using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MessengerPAPS.Models
{
    public class User : IdentityUser
    {
        [Required]
        public int Phone { get; set; }
    }
}