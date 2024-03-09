using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VideoPlatform.WebApp.Data.Entities
{
    public class Notification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [ForeignKey(nameof(Channel))]
        public Guid ReceiverId { get; set; }

        public Channel Receiver { get; set; }

        [StringLength(150, MinimumLength = 1, ErrorMessage = "Message must be between 1 and 150 characters long!")]
        public string Message { get; set; }

        public DateTime ReceivedAt { get; set; }
    }
}
