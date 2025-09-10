using LibraryManagementSystemMVC_Project.Models.RequestModels;
using LibraryManagementSystemMVC_Project.Models.ResponseModels;

namespace LibraryManagementSystemMVC_Project.BAL.ILogic
{
    public interface IApplicationUserRepository
    {

        Task<IEnumerable<ApplicationUserVModel>> GetApplicationUserList();

        Task<ApplicationUserVModel> GetApplicationUserDeatils(string userId);

        Task<int> DeleteApplicationUser(string userId);

        Task<IEnumerable<ApplicationUserVModel>> GetAllUserMembers();
    }
}
