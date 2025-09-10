namespace LibraryManagementSystemMVC_Project.Models.ResponseModels
{
    public class BookVModel
    {
        public Guid BookId { get; set; }
        public string? Title { get; set; } = string.Empty;
        public string? Author { get; set; } = string.Empty;
        public string? Category { get; set; } = string.Empty;
        public int? Quantity { get; set; }
    }
}
