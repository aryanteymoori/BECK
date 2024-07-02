using _0_Framework.Infrastructure;

namespace CommentManagement.Configuration.Permission
{
    public class CommentPermissionExposer : IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>> 
            {
                {
                    "بخش کامنت ها",new List<PermissionDto>
                    {
                        new PermissionDto(CommentPermission.ConfirmComment,"پذیرش مامنت ها"),
                        new PermissionDto(CommentPermission.CancelComment,"کنسل کردن کامنت ها"),
                        new PermissionDto(CommentPermission.ListComment,"گزارش کامنت ها")
                    }
                }
            };
        }
    }
}
