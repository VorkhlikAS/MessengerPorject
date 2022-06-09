using System.ComponentModel.DataAnnotations;

namespace MessengerPAPS.Models
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
