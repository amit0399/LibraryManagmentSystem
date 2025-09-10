using LibraryManagementSystemMVC_Project.Models.EntityModels;

namespace LibraryManagementSystemMVC_Project.Models.RequestModels
{
    public class IssueModel
    {
        
        public Guid BookId { get; set; }
        public Book? Book { get; set; }

        public Guid UserId { get; set; }
        public ApplicationUser? UserName { get; set; }

        public DateTime? IssueDate { get; set; }
        public DateTime? ExpectedReturnDate { get; set; }

        public DateTime? ActualReturnDate { get; set; }
    }
}
