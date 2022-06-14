using System;
using System.ComponentModel.DataAnnotations;

namespace MessengerNetSix.Models
{
    public class Message
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string ChatId { get; set; }
        [Required]
        public string SenderId { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public DateTime PostingDate { get; set; }
    }
}
