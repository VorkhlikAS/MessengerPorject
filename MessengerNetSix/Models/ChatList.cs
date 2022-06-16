using System.ComponentModel.DataAnnotations;

namespace MessengerNetSix.Models
{
    public class ChatList
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Key { get; set; }
        [Required]
        public bool IsAGroup { get; set; }
    }
}
