namespace LibraryManagementSystemMVC_Project.Models.RequestModels
{
    public class BookModel
    {
        public string? Title { get; set; } = string.Empty;
        public string? Author { get; set; } = string.Empty;
        public string? Category { get; set; } = string.Empty;
        public int? Quantity { get; set; }
    }
}
