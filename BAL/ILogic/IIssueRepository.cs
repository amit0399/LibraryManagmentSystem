using LibraryManagementSystemMVC_Project.Models.RequestModels;
using LibraryManagementSystemMVC_Project.Models.ResponseModels;

namespace LibraryManagementSystemMVC_Project.BAL.ILogic
{
    public interface IIssueRepository
    {
        Task<IEnumerable<IssueVModel>> GetIssueBookList();
        Task<IssueVModel> GetIssueBook(string issueId);
        Task<int> CreateIssueBook(IssueModel issueModel);
        Task<int> ConfirmBookReturn(string issueId);
        Task<int> UpdateIssueBook(IssueModel issueModel, string issueId);
        Task<int> DeleteIssueBook(string issueId);
        

    }
}
