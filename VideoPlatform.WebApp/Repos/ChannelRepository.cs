using VideoPlatform.WebApp.Data;
using VideoPlatform.WebApp.Data.Entities;

namespace VideoPlatform.WebApp.Repos
{
    public interface IChannelRepository
    {
        Channel GetChannelById(Guid channelId);
        IEnumerable<Channel> GetAllChannels();
        void Create(Channel channel);
        void Update(Channel channel);
        void Delete(Guid channelId);
    }
    public class ChannelRepository : IChannelRepository 
    {
        private readonly ApplicationDbContext _context;

        public ChannelRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Channel GetChannelByUsername(Guid username)
        {
            return _context.Channels.FirstOrDefault(c => c.Id == username);
        }
        public Channel GetChannelById(Guid channelId)
        {
            return _context.Channels.FirstOrDefault(c => c.Id == channelId);
        }

        public IEnumerable<Channel> GetAllChannels()
        {
            return _context.Channels.ToList();
        }

        public void Create(Channel channel)
        {
            _context.Channels.Add(channel);
            _context.SaveChanges();
        }

        public void Update(Channel channel)
        {
            _context.Channels.Update(channel);
            _context.SaveChanges();
        }

        public void Delete(Guid channelId)
        {
            var channel = _context.Channels.Find(channelId);
            if (channel != null)
            {
                _context.Channels.Remove(channel);
                _context.SaveChanges();
            }
        }

    }
}
