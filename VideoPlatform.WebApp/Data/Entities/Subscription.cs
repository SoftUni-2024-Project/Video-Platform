using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Channels;

namespace VideoPlatform.WebApp.Data.Entities
{
    public class Subscription
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [ForeignKey(nameof(Channel))]
        public Guid SubscriberId { get; set; }

        public Channel Subscriber { get; set; }

        [ForeignKey(nameof(Channel))]
        public Guid ChannelId { get; set; }

        public Channel Channel { get; set; }

        public DateOnly SubscribedSince { get; set; }
    }
}
