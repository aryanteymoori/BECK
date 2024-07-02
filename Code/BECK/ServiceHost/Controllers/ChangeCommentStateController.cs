using CommentManagement.Application.Contract.Comment;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChangeCommentStateController : ControllerBase
    {
        private readonly ICommentApplication _commentApplication;

        public ChangeCommentStateController(ICommentApplication commentApplication)
        {
            _commentApplication = commentApplication;
        }

        [HttpPost("Confirm/{id}")]
        public List<CommentState> Confirm(long id)
        {
            _commentApplication.Confirm(id);
            return _commentApplication.GetCommentsState();
        }

        [HttpPost("Cancel/{id}")]
        public List<CommentState> Cancel(long id)
        {
            _commentApplication.Cancel(id);
            return _commentApplication.GetCommentsState();
        }
    }
}
