using VideoPlatform.WebApp.Model.User;
using VideoPlatform.WebApp.Data.Repositories;
using VideoPlatform.WebApp.Repos;
using Microsoft.AspNetCore.Identity;

namespace VideoPlatform.WebApp.Service
{
    public class ChannelService : IChannelService
    {
        private readonly IVideoRepository _videoRepository;
        private readonly IChannelRepository _channelRepository;

        public ChannelService(IVideoRepository videoRepository, IChannelRepository channelRepository)
        {
            _videoRepository = videoRepository;
            _channelRepository = channelRepository;
        }

        private static List<EditChannelResponceModel> _channels = new List<EditChannelResponceModel>();

        public Task<IdentityResult> CreateUserAsync(RegisterViewModel model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ConfirmEmailAsync(string email, string token)
        {
            throw new NotImplementedException();
        }

        public EditChannelResponceModel EditChannel(EditChannelRequestModel request)
        {
            EditChannelResponceModel channel = _channels.Find(c => c.ChannelId == request.ChannelId);
            if (channel != null)
            {
                channel.Username = request.Username;
                channel.Password = request.Password;    
                channel.Description = request.Description;
                //channel.Privacy = request.Privacy;
                channel.CoverImageUrl = request.CoverImage.ToString();
                channel.UpdatedAt = DateTime.Now;
            }
            return channel;
        }

        public EditChannelResponceModel DeleteChannel(int channelId)
        {
            var channel = _channelRepository.GetChannelById(channelId);
            if (channel == null)
            {
                throw new InvalidOperationException("Channel not found");
            }

            var videos = _videoRepository.GetVideosByChannelId(channelId);
            if (videos.Any())
            {
                throw new InvalidOperationException("Cannot delete channel with associated videos");
            }

            _channelRepository.Delete(channelId);
            throw new InvalidOperationException("Channel deleted");
        }

        public EditChannelResponceModel GetChannelByUsername(string username)
        => _channels.Find(c => c.Username == username);

        public EditChannelResponceModel GetChannelById(int channelId)
            => _channels.Find(c => c.ChannelId == channelId);
    }
}
