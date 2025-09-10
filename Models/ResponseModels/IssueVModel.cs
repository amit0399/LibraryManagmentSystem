using LibraryManagementSystemMVC_Project.Models.EntityModels;

namespace LibraryManagementSystemMVC_Project.Models.ResponseModels
{
    public class IssueVModel
    {
        public Guid IssueId { get; set; }
        public Guid BookId { get; set; }     
        public string? Title { get; set; } = string.Empty; 
        public Guid UserId { get; set; }
        public string? UserName { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime? ExpectedReturnDate { get; set; }
        public DateTime? ActualReturnDate { get; set; }
    }
}
