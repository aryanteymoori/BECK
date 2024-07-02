using _0_Framework.Application;
using _0_Framework.Infrastructure;
using CommentManagement.Application.Contract.Comment;
using CommentManagement.Configuration.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administration.Pages.Comments
{
    [Authorize(Roles =$"{Roles.Administrator},{Roles.ContentUploader}")]
    public class IndexModel : PageModel
    {
        public List<CommentViewModel> Comments { get; set; }
        public string ApiPath {  get; set; }
        private readonly ICommentApplication _commentApplication;
        private readonly IConfiguration _configuration;

        public IndexModel(ICommentApplication commentApplication, IConfiguration configuration)
        {
            _commentApplication = commentApplication;
            _configuration = configuration;
            ApiPath = _configuration.GetSection("WebInfo")["UrlOFApi"];
        }

        [NeedsPermissions(CommentPermission.ListComment)]
		public void OnGet(CommentSearchModel searchModel)
        {
            Comments = _commentApplication.Search(searchModel);
        }
    }
}
