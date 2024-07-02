using _0_Framework.Domain;
using CommentManagement.Application.Contract.Comment;

namespace CommentManagement.Domain.CommentAgg
{
    public interface ICommentRepository:IRepository<Comment,long>
    {
        List<CommentViewModel> Search(CommentSearchModel searchModel);
        List<CommentState> GetCommentsState();
    }
}
