namespace CommentManagement.Application.Contract.Comment
{
    public class CommentState
    {
        public long Id { get; set; }
        public bool IsConfirmed { get; set; }
        public bool IsCanceled { get; set; }
    }
}
