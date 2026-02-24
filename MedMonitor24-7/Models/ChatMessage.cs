using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MedMonitor24_7.Models
{
    [Index(nameof(SenderUserID), nameof(SentAt))]
    public class ChatMessage
    {
        [Key]
        public int MessageID { get; set; }

        [Required]
        public int SenderUserID { get; set; }

        [Required]
        public int ReceiverUserID { get; set; }

        [Required, MaxLength(1000)]
        public string MessageText { get; set; } = null!;

        public DateTime SentAt { get; set; }

        public bool IsFromBot { get; set; }

        public SystemUser SenderUser { get; set; } = null!;
        public SystemUser ReceiverUser { get; set; } = null!;
    }
}
