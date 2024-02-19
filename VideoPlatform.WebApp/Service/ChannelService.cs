using static VideoPlatform.WebApp.Model.User.ChannelRequestModel;
using VideoPlatform.WebApp.Model.User;
using VideoPlatform.WebApp.Data;

namespace VideoPlatform.WebApp.Service
{
    public class ChannelService : IChannelService
    {
        private readonly ApplicationDbContext _context;
        public ChannelService(ApplicationDbContext DBContext)
        {
            _context = DBContext;
        }
        private static List<ChannelResponseModel> _channels = new List<ChannelResponseModel>();

        public ChannelResponseModel CreateChannel(CreateChannelRequestModel request) //suzdavame kanal i go dobavqme
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

            public ChannelResponseModel GetChannelById(int id)
            {
                return _channels.Find(c => c.ChannelId == id);
            }
    }
}
