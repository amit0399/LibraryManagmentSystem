using System.Net.Sockets;
using System.Transactions;
using LibraryManagementSystemMVC_Project.BAL.ILogic;
using LibraryManagementSystemMVC_Project.DatabaseConnection;
using LibraryManagementSystemMVC_Project.Models.EntityModels;
using LibraryManagementSystemMVC_Project.Models.RequestModels;
using LibraryManagementSystemMVC_Project.Models.ResponseModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace LibraryManagementSystemMVC_Project.BAL.Logic
{
    public class IssueRepository : IIssueRepository
    {
        private readonly ILogger<IssueRepository> _logger;
        private readonly DBConnectionFactory _dbconnectionFactory;

        public IssueRepository(ILogger<IssueRepository> logger, DBConnectionFactory dbconnectionFactory)
        {
            _logger = logger;
            _dbconnectionFactory = dbconnectionFactory;
        }

        public async Task<int> ConfirmBookReturn(string issueId)
        {
            try
            {
                var issuedBook = await _dbconnectionFactory.tblIssues.Where(option => option.IssueId == Guid.Parse(issueId) && option.IsDeleted == false).FirstOrDefaultAsync();
                if (issuedBook != null)
                {
                    issuedBook.ActualReturnDate = DateTime.Now;
                    _dbconnectionFactory.tblIssues.Update(issuedBook);
                    await _dbconnectionFactory.SaveChangesAsync();
                }
                else
                { 
                    return 0;
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical(DateTime.Now.ToString() + "Errorn on Issue Repositroy in ConfirmBookReturn Method : " + ex.Message);
            }
            return 1;
        }

        public async Task<int> CreateIssueBook(IssueModel issueModel)
        {
            try
            {
                if (issueModel != null)
                {
                    Issue issue = new Issue()
                    {
                        IssueId = Guid.NewGuid(),
                        BookId = issueModel.BookId,                        
                        UserId = issueModel.UserId,                        
                        IssueDate = issueModel.IssueDate,
                        ExpectedReturnDate = issueModel.ExpectedReturnDate,
                        CreatedOn = DateTime.Now,
                        CreatedBy = "Admin",
                        IsDeleted=false
                    };
                    await _dbconnectionFactory.tblIssues.AddAsync(issue);
                    await _dbconnectionFactory.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical(DateTime.Now.ToString() + "Eror on controller" + ex.Message);
                return 0;
            }
            return 1;
            }

        public async Task<int> DeleteIssueBook(string issueId)
        {
            try
            {
                if (!string.IsNullOrEmpty(issueId))
                {
                    var result = await _dbconnectionFactory.tblIssues.Where(option => option.IssueId == Guid.Parse(issueId)).FirstOrDefaultAsync();
                    if (result != null)
                    {
                        result.IsDeleted = true;
                        result.DeletedOn = DateTime.Now;
                        result.DeletedBy = "Admin";

                        await _dbconnectionFactory.SaveChangesAsync();
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return 0;
                }
                
            
            }
            catch (Exception ex) 
            {
                _logger.LogCritical(DateTime.Now.ToString() + "Error on repository : " + ex.Message);
                return 0;
            }
            return 1;
        }

        public async Task<IssueVModel> GetIssueBook(string issueId)
        {
            IssueVModel issueVModel=new IssueVModel();
            try
            {
                if(!string.IsNullOrEmpty(issueId))
                {
                    issueVModel = await _dbconnectionFactory.tblIssues.Where(option => option.IssueId == Guid.Parse(issueId)).Select(option => new IssueVModel()
                    {
                        IssueId=option.IssueId,
                        Title=option.Book.Title,
                        UserName=option.User.UserName,
                        IssueDate=option.IssueDate,
                        ExpectedReturnDate=option.ExpectedReturnDate,

                    }).FirstOrDefaultAsync();

                }
             
            }
            catch (Exception ex)
            {
                _logger.LogCritical(DateTime.Now.ToString() + "Error on Repository : " + ex.Message);
            }
            return issueVModel;
        }

        public async Task<IEnumerable<IssueVModel>> GetIssueBookList()
        {
            IEnumerable<IssueVModel> issueVModels=new List<IssueVModel>();
            try
            {
                issueVModels = await _dbconnectionFactory.tblIssues.Where(option => option.IsDeleted == false).Select(option => new IssueVModel()
                {
                  IssueId=option.IssueId,  
                  UserName=option.User.UserName,
                  Title=option.Book.Title,
                  IssueDate=option.IssueDate,
                  ExpectedReturnDate =option.ExpectedReturnDate,
                  ActualReturnDate =option.ActualReturnDate
                  
                }).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogCritical(DateTime.Now.ToString()+"Error on Repository : "+ex.Message);
            }
            return issueVModels;
        }

        public async Task<int> UpdateIssueBook(IssueModel issueModel, string issueId)
        {
            try
            {
                var result = await _dbconnectionFactory.tblIssues.Where(option => option.IssueId == Guid.Parse(issueId) && option.IsDeleted == false).FirstOrDefaultAsync();
                if (result != null)
                {
                   
                    
                    result.User= issueModel.UserName;
                    //result.Book.Title = issueModel.Book.Title;
                    result.Book= issueModel.Book;
                    result.IssueDate = issueModel.IssueDate;
                    result.ExpectedReturnDate = issueModel.ExpectedReturnDate;

                    await _dbconnectionFactory.SaveChangesAsync();

                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical(DateTime.Now.ToString() + "Error on Repository : " + ex.Message);
            }
            return 1;
        }
    }
    
}
