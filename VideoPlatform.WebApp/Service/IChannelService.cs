using VideoPlatform.WebApp.Model.User;
using Microsoft.AspNetCore.Identity;
using VideoPlatform.WebApp.Model.AccountModel;

namespace VideoPlatform.WebApp.Service
{
    public interface IChannelService
    {
        Task<IdentityResult> CreateUserAsync(RegisterViewModel model);
        Task<bool> ConfirmEmailAsync(string email, string token);
        EditChannelResponceModel EditChannel(EditChannelRequestModel request);
        EditChannelResponceModel DeleteChannel(int channelId);
        EditChannelResponceModel GetChannelByUsername(string username);
        EditChannelResponceModel GetChannelById(int channelId);
    }
}
