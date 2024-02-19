namespace VideoPlatform.WebApp.Model.User
{
    public class ChannelRequestModel
    {
        public class CreateChannelRequestModel
        {
            public int SubscriberId { get; set; }
            public int? ChannelId { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
            public string Description { get; set; }
            public PrivacyOption Privacy { get; set; }
            public string Email { get; set; } // Added email field
            public bool SendConfirmationEmail { get; set; } // Added option to send confirmation email
            public byte[] CoverImage { get; set; }
        }

        public class EditChannelRequestModel
        {
            public int ChannelId { get; set; }
            public string Description { get; set; }
            public PrivacyOption Privacy { get; set; }
            public byte[] CoverImage { get; set; }
        }
    }
}
