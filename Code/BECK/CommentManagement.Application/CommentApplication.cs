using _0_Framework.Application;
using CommentManagement.Application.Contract.Comment;
using CommentManagement.Domain.CommentAgg;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CommentManagement.Application
{
    public class CommentApplication : ICommentApplication
    {
        private readonly ICommentRepository _commentRepository;

        public CommentApplication(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public OperationResult Add(AddComment command)
        {
            var operation=new OperationResult();

            var comment = new Comment(command.Name,command.Email,command.Message,command.OwnerRecordId,command.Type,command.ParentId);
            _commentRepository.Create(comment);
            _commentRepository.SaveChanges();
            return operation;
        }

        public OperationResult Cancel(long id)
        {
            var operation = new OperationResult();
            var comment = _commentRepository.Get(id);

            if (comment == null)
                return operation.Faile("رکورد مورد نظر یافت نشد");

            comment.Cancel();
            _commentRepository.SaveChanges();
            return operation.Success();
        }
        public OperationResult Confirm(long id)
        {
            var operation = new OperationResult();
            var comment = _commentRepository.Get(id);

            if (comment == null)
                return operation.Faile("رکورد مورد نظر یافت نشد");

            comment.Confirm();
            _commentRepository.SaveChanges();
            return operation.Success();
        }

        public List<CommentState> GetCommentsState()
        {
            return _commentRepository.GetCommentsState();
        }

        public List<CommentViewModel> Search(CommentSearchModel searchModel)
        {
           return _commentRepository.Search(searchModel);
        }
    }
}
