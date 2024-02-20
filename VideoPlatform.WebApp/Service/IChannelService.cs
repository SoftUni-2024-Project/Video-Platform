using static VideoPlatform.WebApp.Model.User.ChannelRequestModel;
using VideoPlatform.WebApp.Model.User;

namespace VideoPlatform.WebApp.Service
{
    public interface IChannelService
    {
        ChannelResponseModel CreateChannel(CreateChannelRequestModel request);
        ChannelResponseModel EditChannel(EditChannelRequestModel request);
        ChannelResponseModel GetChannelByUsername(string username);
        ChannelResponseModel GetChannelById(int channelId);
        ChannelResponseModel DeleteChannel(Guid channelId);
    }
}
