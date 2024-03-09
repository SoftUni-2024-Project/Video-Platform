using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VideoPlatform.WebApp.Data.Entities
{
    public class Video
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string VideoUrl { get; set; }

        [StringLength(70, MinimumLength = 1, ErrorMessage = "Description must be between 1 and 70 characters long!")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "Description must be up to 500 characters long!")]
        public string? Description { get; set; }

        [ForeignKey(nameof(Channel))]
        public Guid ChannelId { get; set; }

        public Channel Channel { get; set; }

        public PrivacyEnum Privacy { get; set; }

        public int Views { get; set; }
    }
}
