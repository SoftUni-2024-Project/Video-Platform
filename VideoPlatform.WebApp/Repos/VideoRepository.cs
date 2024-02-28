using VideoPlatform.WebApp.Data.Entities;

namespace VideoPlatform.WebApp.Data.Repositories
{
    public interface IVideoRepository
    {
        Video GetVideoById(Guid videoId);
        IEnumerable<Video> GetAllVideos();
        void Create(Video video);
        void Update(Video video);
        void Delete(Video video);
        public IEnumerable<Video> GetVideosByChannelId(int channelId);
    }

    public class VideoRepository : IVideoRepository
    {
        private readonly ApplicationDbContext _context;

        public VideoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Video GetVideoById(Guid videoId)
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

        public IEnumerable<Video> GetVideosByChannelId(int channelId)
        {
            return _context.Videos.Where(v => v.ChannelId.Equals(channelId)).ToList();
        }
    }
}
