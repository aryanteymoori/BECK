using _0_Framework.Application;
using _0_Framework.Infrastructure;
using CommentManagement.Application.Contract.Comment;
using CommentManagement.Domain.CommentAgg;

namespace CommentManagement.Infrastructure.EFCore.Repository
{
    public class CommentRepository : RepositoryBase<Comment, long>, ICommentRepository
    {
        private readonly CommentContext _commentContext;

        public CommentRepository(CommentContext commentContext) : base(commentContext)
        {
            _commentContext = commentContext;
        }

        public List<CommentState> GetCommentsState()
        {
            return _commentContext.Comments.Select(x => new CommentState 
            {
                Id=x.Id,
                IsCanceled = x.IsCanceled,
                IsConfirmed= x.IsConfirmed,
            }).ToList();
        }

        public List<CommentViewModel> Search(CommentSearchModel searchModel)
        {
            var query = _commentContext.Comments.Select(x => new CommentViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email,
                IsCanceled = x.IsCanceled,
                IsConfirmed = x.IsConfirmed,
                Message = x.Message,
                OwnerRecordId = x.OwnerRecordId,
                ParentId = x.ParentId,
                Type = x.Type,
                CreationDate=x.CreateionDate.ToFarsi()
            });

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(x => x.Name.Contains(searchModel.Name));

            if (!string.IsNullOrWhiteSpace(searchModel.Email))
                query = query.Where(x => x.Email.Contains(searchModel.Email));

            return query.OrderByDescending(x=>x.Id).ToList();
        }
    }
}
