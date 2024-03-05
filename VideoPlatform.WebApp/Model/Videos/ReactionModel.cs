namespace VideoPlatform.WebApp.Model.Videos
{
    public class ReactionModel
    {
        public Guid VideoId { get; set; }
        public Guid ChannelId { get; set; }
        public bool Reaction { get; set; }
    }
}
