using static VideoPlatform.WebApp.Model.User.ChannelRequestModel;
using VideoPlatform.WebApp.Model.User;
using VideoPlatform.WebApp.Data.Repositories;
using VideoPlatform.WebApp.Repos;

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

        private static List<ChannelResponseModel> _channels = new List<ChannelResponseModel>();

        public ChannelResponseModel CreateChannel(CreateChannelRequestModel request)
            {
                ChannelResponseModel newChannel = new ChannelResponseModel
                {
                    ChannelId = _channels.Count + 1,
                    Username = request.Username,
                    Description = request.Description,
                    Privacy = request.Privacy,
                    CoverImageUrl = "",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                _channels.Add(newChannel);

                
                if (request.SendConfirmationEmail) //confirm email
                {
                    
                }

                return newChannel;
            }

        public ChannelResponseModel EditChannel(EditChannelRequestModel request)
        {
                ChannelResponseModel channel = _channels.Find(c => c.ChannelId == request.ChannelId);
                if (channel != null)
                {
                    channel.Description = request.Description;
                    channel.Privacy = request.Privacy;
                    // for some reason ne mi dava da sloja coverImage kato neshto koeto moje da se promenq
                    channel.UpdatedAt = DateTime.Now;
                }
                return channel;
        }

        public ChannelResponseModel GetChannelByUsername(string username) => _channels.Find(c => c.Username == username);

        public ChannelResponseModel DeleteChannel(Guid channelId)
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

        public ChannelResponseModel GetChannelById(int channelId)
         => _channels.Find(c => c.ChannelId == channelId);
    }
}
