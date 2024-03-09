using Microsoft.AspNetCore.Mvc;
using VideoPlatform.WebApp.Service;
using VideoPlatform.WebApp.Model.Videos;
using VideoPlatform.WebApp.Data.Entities;

namespace VideoPlatform.WebApp.Controllers
{
    public class VideoController : Controller
    {
            private readonly IVideoService _videoService;
            private readonly ICommentService _commentService;

            public VideoController(IVideoService videoService, ICommentService commentService)
            {
                _videoService = videoService;
                _commentService = commentService;
            }

            [HttpGet("{videoId}")]
            public IActionResult GetVideo(Guid videoId)
            {
                var video = _videoService.GetVideo(videoId);
                if (video == null)
                    return NotFound();

                return View(video);
            }

            [HttpGet]
            public IActionResult GetAllVideos()
            {
                var videos = _videoService.GetAllVideos();
                return View(videos);
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
            [HttpPost("like")]
            public IActionResult LikeVideo(Guid videoId, Guid channelId, VideoReaction reaction)
            {
                try
            {
                    _videoService.LikeVideo(videoId, channelId, reaction);
                    return Ok("Video liked successfully!");
            }
                catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            }

            [HttpPost("dislike")]
            public IActionResult DislikeVideo(Guid videoId, Guid channelId, VideoReaction reaction)
            {
                try
                {
                _videoService.DislikeVideo(videoId, channelId, reaction);
                return Ok("Video disliked successfully!");
                }
                catch (ArgumentException ex)
                {
                return BadRequest(ex.Message);
                }
            }

        [HttpGet("likeCount")]
        public IActionResult GetLikeCount(Guid videoId)
        {
            var count = _videoService.GetLikeCount(videoId);
            return Ok($"Like count for video with ID {videoId}: {count}");
        }

        [HttpGet("dislikeCount")]
        public IActionResult GetDislikeCount(Guid videoId)
        {
            var count = _videoService.GetDislikeCount(videoId);
            return Ok($"Dislike count for video with ID {videoId}: {count}");
        }

        [HttpPost]
        public IActionResult AddComment([FromBody] Comment comment)
        {
            try
            {
                _commentService.AddComment(comment);
                return Ok("Comment added successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{commentId}")]
        public IActionResult DeleteComment(Guid commentId)
        {
            try
            {
                _commentService.DeleteComment(commentId);
                return Ok("Comment deleted successfully");
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("video/{videoId}")]
        public IActionResult GetCommentsForVideo(Guid videoId)
        {
            try
            {
                var comments = _commentService.GetCommentsForVideo(videoId);
                return Ok(comments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("reply/{parentCommentId}")]
        public IActionResult ReplyToComment(Guid parentCommentId, [FromBody] Comment reply)
        {
            try
            {
                _commentService.ReplyToComment(parentCommentId, reply);
                return Ok("Reply added successfully");
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut]
        public IActionResult UpdatingComment([FromBody] Comment comment)
        {
            try
            {
                _commentService.UpdatingComment(comment);
                return Ok("Comment updated successfully");
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
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
