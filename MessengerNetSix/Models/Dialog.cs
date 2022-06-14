using System.ComponentModel.DataAnnotations;

namespace MessengerNetSix.Models
{
    public class Dialog
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
