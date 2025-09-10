namespace LibraryManagementSystemMVC_Project.Models.ResponseModels
{
    public class ApplicationUserVModel
    {
        public Guid UserId { get; set; }
        public string? UserName { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;  
        public string? Role { get; set; } = string.Empty;
      
    }
}
