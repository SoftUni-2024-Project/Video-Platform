using VideoPlatform.WebApp.Data.Entities;
using VideoPlatform.WebApp.Repos;

namespace VideoPlatform.WebApp.Service
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public void AddComment(Comment comment)
        {
            _commentRepository.AddComment(comment);
        }

        public void DeleteComment(Guid commentId)
        {
            var commentToDelete = _commentRepository.GetCommentById(commentId);
            if (commentToDelete != null)
            {
                _commentRepository.DeleteComment(commentId);
            }
            else
            {
                throw new ArgumentException("Comment not found");
            }
        }

        public IEnumerable<Comment> GetCommentsForVideo(Guid videoId)
        {
            return _commentRepository.GetCommentsForVideo(videoId);
        }

        public void ReplyToComment(Guid parentCommentId, Comment reply)
        {
            var parentComment = _commentRepository.GetCommentById(parentCommentId);
            if (parentComment != null)
            {
                reply.ParentCommentId = parentCommentId;
                _commentRepository.AddComment(reply);
            }
            else
            {
                throw new ArgumentException("Parent comment not found");
            }
        }

        public void UpdatingComment(Comment comment)
        {
            var existingComment = _commentRepository.GetCommentById(comment.Id);
            if (existingComment != null)
            {
                existingComment.Message = comment.Message;
                _commentRepository.UpdateComment(existingComment);
            }
            else
            {
                throw new ArgumentException("Comment not found");
            }
        }
    }
}
