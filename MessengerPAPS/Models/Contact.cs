using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MessengerPAPS.Models
{
    public class Contact
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string SenderId { get; set; }
        [Required]
        public string RecieverId { get; set; }
        public bool IsConfirmed { get; set; }
        public bool Forbiden { get; set; }
    }
}
