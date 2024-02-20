using VideoPlatform.WebApp.Data.Entities;
using VideoPlatform.WebApp.Model;
using VideoPlatform.WebApp.Model.Videos;

namespace VideoPlatform.WebApp.Service
{
    public interface IVideoService
    {
        VideoResponseModel GetVideo(Guid videoId);
        IEnumerable<VideoResponseModel> GetAllVideos();
        void CreateVideo(VideoRequestModel videoRequest);
        void UpdateVideo(Guid videoId, VideoRequestModel videoRequest);
        void DeleteVideo(Guid videoId);
        IEnumerable<Video> GetVideosByChannelId(Guid channelId);
    }
}
