using LibraryManagementSystemMVC_Project.BAL.ILogic;
using LibraryManagementSystemMVC_Project.DatabaseConnection;
using LibraryManagementSystemMVC_Project.Models.ResponseModels;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystemMVC_Project.BAL.Logic
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly ILogger<DashboardRepository> _logger;
            
        private readonly DBConnectionFactory _dbConnectionFactory;


        public DashboardRepository(ILogger<DashboardRepository> logger, DBConnectionFactory dbConnectionFactory)
        {
            _logger = logger;
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<List<BookInventoryVModel>> GetBookInventory()
        {
            var books = await _dbConnectionFactory.tblBooks.Where(option => option.IsDeleted == false).ToListAsync();
            var issued = await _dbConnectionFactory.tblIssues.Where(option => option.IsDeleted == false && option.ActualReturnDate == null).ToListAsync();
            
            List<BookInventoryVModel> bookInventoryVModels=new List<BookInventoryVModel>();

            foreach (var book in books)
            {
                int issuedCount = issued.Count(option => option.BookId == book.BookId);
                int totalCopies = book.Quantity ?? 0;
                int availableCopies = totalCopies - issuedCount;

                bookInventoryVModels.Add(new BookInventoryVModel
                {
                    BookId = book.BookId,
                    Title = book.Title,
                    TotleCopies = totalCopies,
                    TotleIssuedCopies=issuedCount,
                    TotleAvailableCopies=availableCopies < 0 ? 0: availableCopies
                });
            }
            return bookInventoryVModels;
        }

        public async Task<DashboardVModel> AdminDashboard()
        {
            DashboardVModel dashboardVModel=new DashboardVModel();
            try
            {
                
                var totalBooks = await _dbConnectionFactory.tblBooks.Where(option => option.IsDeleted == false).SumAsync(option => option.Quantity ?? 0);
                var totalMembers=await _dbConnectionFactory.tblApplicationUsers.Where(option=>option.IsDeleted==false).Select(option=>new DashboardVModel()).CountAsync();
                var totalIssuedBooks = await _dbConnectionFactory.tblIssues.Where(option=>option.IsDeleted==false && option.ActualReturnDate==null).CountAsync();
                var totalAvailableBooks = totalBooks - totalIssuedBooks;


                dashboardVModel.TotalBooks = totalBooks;
                dashboardVModel.TotalMembers = totalMembers;
                dashboardVModel.TotalIssuedBook = totalIssuedBooks;
                dashboardVModel.TotalAvailableBook = totalAvailableBooks;
                
            }
            catch (Exception ex)
            {

                throw;
            }
            return dashboardVModel;
        }

        public async Task<List<UserDashboardVModel>> UserDashboard(string userId)
        {
            List<UserDashboardVModel> userDashboardVModel = new List<UserDashboardVModel>();
            try
            {
                var issuedBooks = await _dbConnectionFactory.tblIssues.Where(option => option.UserId == Guid.Parse(userId)).ToListAsync();

                foreach (var issue in issuedBooks)
                {
                    var book = await _dbConnectionFactory.tblBooks.FindAsync(issue.BookId);
                    if(book != null)
                    {
                       userDashboardVModel.Add(new UserDashboardVModel
                       {                         
                           Title=book.Title,
                           IssueDate=issue.IssueDate,
                           ExpectedReturnDate=issue.ExpectedReturnDate,
                           ActualReturnDate=issue.ActualReturnDate
                        });
                        
                    }
                }
                
            }
            catch (Exception ex)
            {
                _logger.LogCritical(DateTime.Now.ToString()+"Error on Dashboard Repository in UserDashboard  method :  "+ex.Message); 
            }
            return userDashboardVModel;
        }
    }
}
