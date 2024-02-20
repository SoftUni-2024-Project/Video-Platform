using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TimeoutEntity = VideoPlatform.WebApp.Data.Entities.Timeout;
using VideoPlatform.WebApp.Data.Entities;

namespace VideoPlatform.WebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<Channel, Role, Guid>
    {
        public DbSet<Subscription> Subscriptions { get; set; }

        public DbSet<Ban> Bans { get; set; }

        public DbSet<Block> Blocks {get; set; }

        public DbSet<TimeoutEntity> Timeouts {get; set; }

        public DbSet<Video> Videos {get; set; }

        public DbSet<Playlist> Playlists {get; set; }

        public DbSet<PlaylistsVideos> PlaylistsVideos {get; set; }

        public DbSet<Comment> Comments {get; set; }

        public DbSet<VideoReaction> VideoReactions {get; set; }

        public DbSet<CommentReaction> CommentReactions {get; set; }

        public DbSet<Notification> Notifications {get; set; }
        public DbSet<Channel> Channels { get; set; }    

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    }
}