using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VideoPlatform.WebApp.Data.Entities
{
    public class Ban
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [ForeignKey(nameof(Channel))]
        public Guid ChannelId { get; set; }

        public Channel Channel { get; set; }

        public DateOnly BannedSince { get; set; }
    }
}
