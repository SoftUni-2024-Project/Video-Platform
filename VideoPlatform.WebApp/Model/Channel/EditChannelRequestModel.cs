namespace VideoPlatform.WebApp.Model.User
{
    public class EditChannelRequestModel
    {
            public int? ChannelId { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
            public string Description { get; set; }
            public PrivacyOption Privacy { get; set; }
            public byte[] CoverImage { get; set; }
    }
}
