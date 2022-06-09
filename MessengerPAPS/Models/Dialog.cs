using System.ComponentModel.DataAnnotations;

namespace MessengerPAPS.Models
{
    public class Dialog
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
