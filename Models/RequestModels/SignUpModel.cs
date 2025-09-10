using LibraryManagementSystemMVC_Project.Models.EntityModels;

namespace LibraryManagementSystemMVC_Project.Models.RequestModels
{
    public class SignUpModel:CommonEntity
    {
        public string UserName { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
