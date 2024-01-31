using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VideoPlatform.WebApp.Data.Entities
{
    public class PlaylistsVideos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [ForeignKey(nameof(Playlist))]
        public Guid PlaylistId { get; set; }

        public Playlist Playlist { get; set; }

        [ForeignKey(nameof(Video))]
        public Guid VideoId { get; set; }

        public Video Video { get; set; }

        public int Position { get; set; }
    }
}
