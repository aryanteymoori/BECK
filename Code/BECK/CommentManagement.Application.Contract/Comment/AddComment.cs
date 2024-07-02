using System.ComponentModel.DataAnnotations;

namespace CommentManagement.Application.Contract.Comment
{
    public class AddComment
    {
        [Required(ErrorMessage ="این فیلد نمیتواند خالی باشد")]
        public string Name { get; set; }
        [Required(ErrorMessage ="این فیلد نمیتواند خالی باشد")]
        public string Email { get; set; }
        [Required(ErrorMessage ="این فیلد نمیتواند خالی باشد")]
        public string Message { get; set; }
        public bool IsConfirmed { get; set; }
        public bool IsCanceled { get; set; }
        public long OwnerRecordId { get; set; }
        public int Type { get; set; }
        public long ParentId { get; set; }
    }
}
