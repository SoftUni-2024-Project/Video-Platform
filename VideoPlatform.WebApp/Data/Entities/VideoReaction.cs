using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VideoPlatform.WebApp.Data.Entities
{
    public class VideoReaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [ForeignKey(nameof(Video))]
        public Guid VideoId { get; set; }

        public Video Video { get; set; }

        [ForeignKey(nameof(Channel))]
        public Guid ChannelId { get; set; }

        public Channel Channel { get; set; }

        // TODO: Трябва да го измислим това дали ще е стринг, или нещо друго
        public bool Reaction { get; set; }
    }
}
