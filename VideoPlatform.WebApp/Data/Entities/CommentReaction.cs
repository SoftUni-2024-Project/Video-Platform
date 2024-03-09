using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VideoPlatform.WebApp.Data.Entities
{
    public class CommentReaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [ForeignKey(nameof(Comment))]
        public Guid CommentId { get; set; }

        public Comment Comment { get; set; }

        [ForeignKey(nameof(Channel))]
        public Guid ChannelId { get; set; }

        public Channel Channel { get; set; }

        // TODO: Трябва да го измислим това дали ще е стринг, или нещо друго
        public string Reaction { get; set; }
    }
}
