using VideoPlatform.WebApp.Data.Entities;
using VideoPlatform.WebApp.Model.Videos;

namespace VideoPlatform.WebApp.Service
{
    public interface IVideoService
    {
        VideoResponseModel GetVideo(Guid videoId);
        IEnumerable<VideoResponseModel> GetAllVideos();
        void CreateVideo(VideoRequestModel videoRequest);
        void UpdateVideo(Guid videoId, VideoRequestModel videoRequest);
        void LikeVideo(Guid videoId, Guid channelId, VideoReaction reaction);
        void DislikeVideo(Guid videoId, Guid channelId, VideoReaction reaction);
        int GetLikeCount(Guid videoId);
        int GetDislikeCount(Guid videoId);
        void DeleteVideo(Guid videoId);
        void Notification(Guid videoId);
        IEnumerable<Video> GetVideosByChannelId(int channelId);
    }
}
