using VideoPlatform.WebApp.Data.Entities;

namespace VideoPlatform.WebApp.Service
{
    public interface ICommentService
    {
        IEnumerable<Comment> GetCommentsForVideo(Guid videoId);
        void AddComment(Comment comment);
        void UpdatingComment(Comment comment);  
        void DeleteComment(Guid commentId);
        void ReplyToComment(Guid parentCommentId, Comment reply);
    }
}
