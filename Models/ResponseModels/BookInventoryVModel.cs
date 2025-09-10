namespace LibraryManagementSystemMVC_Project.Models.ResponseModels
{
    public class BookInventoryVModel
    {
        public Guid BookId { get; set; }
        public string? Title { get; set; } = string.Empty;
        public int? TotleCopies { get; set; }
        public int? TotleIssuedCopies { get; set; }
        public int? TotleAvailableCopies { get; set; }

    }
}
