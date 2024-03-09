using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using VideoPlatform.WebApp.Data.Entities;
using VideoPlatform.WebApp.Data.Repositories;
using VideoPlatform.WebApp.Model.Videos;
using VideoPlatform.WebApp.Services;

namespace VideoPlatform.WebApp.Service
{
    public class VideoService : IVideoService
    {
            private readonly IVideoRepository _videoRepository;
            private readonly IEmailService _emailService;

            public VideoService(IVideoRepository videoRepository, IEmailService emailService)
            {
                _videoRepository = videoRepository;
                _emailService = emailService;
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
                Notification(video.Id);
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

        public void LikeVideo(Guid videoId, Guid channelId, VideoReaction reaction)
        {
            if (!_videoRepository.Exists(videoId, channelId))
            {
                _videoRepository.LikeVideo(reaction);
            }
            else
            {
                throw new ArgumentException("Already liked the video!");
            }
        }
        public void DislikeVideo(Guid videoId, Guid channelId, VideoReaction reaction)
        {
            if (!_videoRepository.Exists(videoId, channelId))
            {
                _videoRepository.DislikeVideo(reaction);
            }
            else
            {
                throw new ArgumentException("Already disliked the video!");
            }
        }
        public void Notification(Guid videoId)
        {
            Video video = _videoRepository.GetVideoById(videoId);
            List<Subscription> subscriptions = _videoRepository.GetAllSubscriptions(video.ChannelId); 
            foreach(var subscription in subscriptions)
            {
                var message = new Message((new string[] { subscription.Subscriber.Email! }).ToList(), "new video dropped", video.VideoUrl);
                _emailService.SendEmail(message);
            }
            
        }
        public int GetLikeCount(Guid videoId)
        {
            return _videoRepository.GetLikeCount(videoId);
        }

        public int GetDislikeCount(Guid videoId)
        {
            return _videoRepository.GetDislikeCount(videoId);
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
             IEnumerable<Video> IVideoService.GetVideosByChannelId(int channelId)
             {
                return _videoRepository.GetVideosByChannelId(channelId);
             }
    }
}
