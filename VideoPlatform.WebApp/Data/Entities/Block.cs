using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VideoPlatform.WebApp.Data.Entities
{
    public class Block
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [ForeignKey(nameof(Channel))]
        public Guid BlockerId { get; set; }

        public Channel Blocker { get; set; }

        [ForeignKey(nameof(Channel))]
        public Guid BlockedId { get; set; }

        public Channel Blocked { get; set; }

        public DateOnly BlockedSince { get; set; }
    }
}
