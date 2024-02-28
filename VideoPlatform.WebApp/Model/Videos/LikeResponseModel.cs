namespace VideoPlatform.WebApp.Model.Videos
{
    public class LikeResponseModel
    {
        public int VideoId { get; set; }
        public bool IsLiked { get; set; }
        public int LikesCount { get; set; }
    }
}
