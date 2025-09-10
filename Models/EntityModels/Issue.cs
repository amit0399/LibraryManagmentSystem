namespace LibraryManagementSystemMVC_Project.Models.EntityModels
{
    public class Issue :CommonEntity
    {
        public Guid IssueId { get; set; }
        public Guid BookId { get; set; }
        public Book? Book { get; set; }

        public Guid UserId { get; set; }
        public ApplicationUser? User { get; set; }

        public DateTime? IssueDate { get; set; }
        public DateTime? ExpectedReturnDate { get; set; }
        public DateTime? ActualReturnDate { get; set; }
    }
}
