using VideoPlatform.WebApp.Data.Entities;

namespace VideoPlatform.WebApp.Data.Repositories
{
    public interface IVideoRepository
    {
        Video? GetVideoById(Guid videoId);
        IEnumerable<Video> GetAllVideos();
        void Create(Video video);
        void Update(Video video);
        void Delete(Video video);
        void LikeVideo(VideoReaction reaction);
        void DislikeVideo(VideoReaction reaction);
        int GetLikeCount(Guid videoId);
        int GetDislikeCount(Guid videoId);
        public IEnumerable<Video> GetVideosByChannelId(int channelId);
        bool Exists(Guid videoId, Guid channelId);
    }

    public class VideoRepository : IVideoRepository
    {
        private readonly ApplicationDbContext _context;

        public VideoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Video? GetVideoById(Guid videoId)
        {
            return _context.Videos.FirstOrDefault(v => v.Id == videoId);
        }

        public IEnumerable<Video> GetAllVideos()
        {
            return _context.Videos.ToList();
        }

        public void Create(Video video)
        {
            _context.Videos.Add(video);
            _context.SaveChanges();
        }

        public void Update(Video video)
        {
            _context.Videos.Update(video);
            _context.SaveChanges();
        }

        public void Delete(Video video)
        {
            _context.Videos.Remove(video);
            _context.SaveChanges();
        }

        public bool Exists(Guid videoId, Guid channelId)
        {
        return _context.Reactions.Any(l => l.VideoId.Equals(videoId) && l.ChannelId.Equals(channelId));
        }

        public void LikeVideo(VideoReaction reaction)
        {
        _context.Reactions.Add(reaction);
        _context.SaveChanges();
        }
        public void DislikeVideo(VideoReaction reaction)
        {
        _context.Reactions.Add(reaction);
        _context.SaveChanges();
        }
        public int GetLikeCount(Guid videoId)
        {
            return _context.Reactions.Count(r => r.VideoId == videoId && r.Reaction);
        }

        public int GetDislikeCount(Guid videoId)
        {
            return _context.Reactions.Count(r => r.VideoId == videoId && !r.Reaction);
        }
        public IEnumerable<Video> GetVideosByChannelId(int channelId)
        {
            return _context.Videos.Where(v => v.ChannelId.Equals(channelId)).ToList();
        }
    }
}
