using System.ComponentModel.DataAnnotations;

namespace MessengerNetSix.Models
{
    public class ChatMember
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public int ChatId { get; set; }
        public string ChatName { get; set; }
    }
}
