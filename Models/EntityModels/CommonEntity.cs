namespace LibraryManagementSystemMVC_Project.Models.EntityModels
{
    public class CommonEntity
    {
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; } = string.Empty;
        public DateTime? UpdatedOn { get; set; }
        public string? UpdatedBy { get; set; } = string.Empty ;
        public bool? IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string? DeletedBy { get; set; } = string.Empty; 
        public string? Remarks { get; set; } = string.Empty;
    }
}
