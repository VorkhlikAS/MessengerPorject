using System.ComponentModel.DataAnnotations;

namespace MessengerNetSix.Models
{
    public class GroupChat
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string AdminId  { get; set; }
    }
}
