namespace VideoPlatform.WebApp.Model.User
{
    public class ChannelResponseModel
    {
            public int ChannelId { get; set; }
            public string Username { get; set; }
            public string Description { get; set; }
            public PrivacyOption Privacy { get; set; }
            public string CoverImageUrl { get; set; }
            public DateTime CreatedAt { get; set; }
            public DateTime UpdatedAt { get; set; }
    }

        public enum PrivacyOption
        {
            Public,
            Private
        }
    }
