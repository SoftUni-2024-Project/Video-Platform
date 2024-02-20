using VideoPlatform.WebApp.Data.Entities;
using VideoPlatform.WebApp.Data.Repositories;
using VideoPlatform.WebApp.Model.Videos;

namespace VideoPlatform.WebApp.Service
{
    public class VideoService : IVideoService
    {
            private readonly IVideoRepository _videoRepository;

            public VideoService(IVideoRepository videoRepository)
            {
                _videoRepository = videoRepository;
            }

            public void CreateVideo(VideoRequestModel videoRequest)
            {
                var video = new Video
                {
                    Name = videoRequest.Name,
                    Description = videoRequest.Description,
                    ChannelId = videoRequest.ChannelId,
                    Privacy = videoRequest.Privacy,
                    Views = 0, 
                };

                _videoRepository.Create(video);
            }

            public void UpdateVideo(Guid videoId, VideoRequestModel videoRequest)
            {
                var existingVideo = _videoRepository.GetVideoById(videoId);
                if (existingVideo == null)
                    throw new Exception("Video not found");

                existingVideo.Name = videoRequest.Name;
                existingVideo.Description = videoRequest.Description;
                existingVideo.ChannelId = videoRequest.ChannelId;
                existingVideo.Privacy = videoRequest.Privacy;

                _videoRepository.Update(existingVideo);
            }

            public void DeleteVideo(Guid videoId)
            {
                var existingVideo = _videoRepository.GetVideoById(videoId);
                if (existingVideo == null)
                    throw new Exception("Video not found");

                _videoRepository.Delete(existingVideo);
            }

            VideoResponseModel IVideoService.GetVideo(Guid videoId)
            {
                var video = _videoRepository.GetVideoById(videoId);
                if (video == null)
                    return null;

            return new VideoResponseModel
            {
                Id = video.Id,
                Name = video.Name,
                Description = video.Description,
                ChannelId = video.ChannelId,
                Privacy = video.Privacy,
                Views = video.Views,
            };
            }

             IEnumerable<VideoResponseModel> IVideoService.GetAllVideos()
             {
                var videos = _videoRepository.GetAllVideos();
                return videos.Select(video => new VideoResponseModel
                {
                Id = video.Id,
                Name = video.Name,
                Description = video.Description,
                ChannelId = video.ChannelId,
                Privacy = video.Privacy,
                Views = video.Views,
                });
             }
             IEnumerable<Video> IVideoService.GetVideosByChannelId(Guid channelId)
             {
                return _videoRepository.GetVideosByChannelId(channelId);
             }
    }
}
