using Microsoft.EntityFrameworkCore;
using VideoPlatform.WebApp.Data;
using Channel = VideoPlatform.WebApp.Data.Entities.Channel;

namespace VideoPlatform.WebApp.Repos
{
    public interface IChannelRepository
    {
        Channel GetChannelByUsermane(Guid username);
        Channel GetChannelById(int channelId);
        IEnumerable<Channel> GetAllChannels();
        void Register(Channel channel);
        void Edit(Channel channel);
        void Delete(int channelId);
    }
    public class ChannelRepository : IChannelRepository 
    {
        private List<Channel> _channels= new List<Channel>();
        private readonly ApplicationDbContext _context;

        public ChannelRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Register(Channel channel)
        {
            if (channel == null)
            {
                throw new ArgumentNullException(nameof(channel), "User cannot be null.");
            }
            if (string.IsNullOrWhiteSpace(channel.Username))
            {
                throw new ArgumentException("Username cannot be empty or whitespace.", nameof(channel.Username));
            }

            if (string.IsNullOrWhiteSpace(channel.Email))
            {
                 throw new ArgumentException("Email cannot be empty or whitespace.", nameof(channel.Email));
            }

            if (channel.Password.ToString().Length < 8)
            {
                throw new ArgumentException("Password must be at least 8 characters long.", nameof(channel.Password));
            }

            _channels.Add(channel);
        }
        public void Edit(Channel channel)
        {
            _context.Channels.Update(channel);
            _context.SaveChanges();
        }
        public void Delete(int channelId)
        {
            var channelToDelete = _context.Channels.FirstOrDefault(c => c.Id.Equals(channelId));
            if (channelToDelete != null)
            {
                _context.Channels.Remove(channelToDelete);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Channel> GetAllChannels()
        {
            return _context.Channels.ToList();
        }

        public Channel GetChannelByUsermane(Guid username)
        {
            return _context.Channels.FirstOrDefault(c => c.Id == username);
        }

        public Channel GetChannelById(int channelId)
        {
            Channel channel = _context.Channels.FirstOrDefault(c => c.Id.Equals(channelId));
            return channel;
        }

    }
}
