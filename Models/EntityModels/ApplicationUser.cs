using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystemMVC_Project.Models.EntityModels
{
    public class ApplicationUser : CommonEntity
    {
        [Key]
        public Guid UserId { get; set; }
        public string? UserName { get; set; } = string.Empty;   
        public string? Email { get; set; } = string.Empty;
        public string? PasswordHash { get; set; }=string.Empty;
        public string? Role { get; set; }=string.Empty;
        public bool? IsLogin { get; set; }
        public DateTime? LoginOn { get; set; }
        public bool? IsBlocked { get; set; }        
        public bool? IsActive { get; set; }

    }
}
