using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VideoPlatform.WebApp.Data.Entities
{
    public class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [ForeignKey(nameof(Channel))]
        public Guid CommenterId { get; set; }

        public Channel Commenter { get; set; }

        [ForeignKey(nameof(Video))]
        public Guid VideoId { get; set; }

        public Video Video { get; set; }

        [ForeignKey(nameof(Comment))]
        public Guid? ParentCommentId { get; set; }

        public Comment? ParentComment { get; set; }

        [StringLength(350, MinimumLength = 1, ErrorMessage = "Message must be between 1 and 350 characters long!")]
        public string Message { get; set; }

        public DateOnly DatePosted { get; set; }
    }
}
