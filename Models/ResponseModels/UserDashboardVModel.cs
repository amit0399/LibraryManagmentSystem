namespace LibraryManagementSystemMVC_Project.Models.ResponseModels
{
    public class UserDashboardVModel
    {
        
        public string? Title { get; set; } = string.Empty;
        public DateTime? IssueDate { get; set; }
        public DateTime? ExpectedReturnDate { get; set; }
        public DateTime? ActualReturnDate { get; set; }
    }
}
