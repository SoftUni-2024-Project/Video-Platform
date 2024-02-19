using VideoPlatform.WebApp.Data;

namespace VideoPlatform.WebApp.Model.Videos
{
    public class VideoRequestModel
    {
            public string Name { get; set; }
            public string Description { get; set; }
            public Guid ChannelId { get; set; }
            public PrivacyEnum Privacy { get; set; }

    }
}
