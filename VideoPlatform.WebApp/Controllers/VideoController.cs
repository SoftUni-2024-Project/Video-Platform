using Microsoft.AspNetCore.Mvc;
using VideoPlatform.WebApp.Service;
using VideoPlatform.WebApp.Model.Videos;

namespace VideoPlatform.WebApp.Controllers
{
    public class VideoController : Controller
    {
            private readonly IVideoService _videoService;

            public VideoController(IVideoService videoService)
            {
                _videoService = videoService;
            }

            [HttpGet("{videoId}")]
            public IActionResult GetVideo(Guid videoId)
            {
                var video = _videoService.GetVideo(videoId);
                if (video == null)
                    return NotFound();

                return Ok(video);
            }

            [HttpGet]
            public IActionResult GetAllVideos()
            {
                var videos = _videoService.GetAllVideos();
                return Ok(videos);
            }

            [HttpPost]
            [Route ("create")]
            public IActionResult CreateVideo(VideoRequestModel videoRequest)
            {
                _videoService.CreateVideo(videoRequest);
                return CreatedAtAction(nameof(GetVideo), new { videoId = videoRequest.ChannelId }, videoRequest);
            }

            [HttpPut]
            [Route("update")]
            public IActionResult UpdateVideo(Guid videoId, VideoRequestModel videoRequest)
            {
                _videoService.UpdateVideo(videoId, videoRequest);
                return NoContent();
            }

            [HttpDelete]
            [Route("delete")]
            public IActionResult DeleteVideo(Guid videoId)
            {
                _videoService.DeleteVideo(videoId);
                return NoContent();
            }
    }

}
