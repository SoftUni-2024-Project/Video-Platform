using VideoPlatform.WebApp.Data;

namespace VideoPlatform.WebApp.Model.Videos
{
    public class VideoResponseModel
    {
        public Guid Id { get; set; }
        public string VideoUrl { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid ChannelId { get; set; }
        public PrivacyEnum Privacy { get; set; }
        public int Views { get; set; }
    }
}
