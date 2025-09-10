using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystemMVC_Project.Models.EntityModels
{
    public class Book :CommonEntity
    {
        [Key]
        public Guid BookId { get; set; }
        public string? Title { get; set; } = string.Empty;
        public string? Author { get; set; } = string.Empty;
        public string? Category { get; set; } = string.Empty;
        public int? Quantity { get; set; }


    }
}
