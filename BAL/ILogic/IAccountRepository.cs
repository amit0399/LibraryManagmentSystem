using LibraryManagementSystemMVC_Project.Models.RequestModels;
using LibraryManagementSystemMVC_Project.Models.ResponseModels;

namespace LibraryManagementSystemMVC_Project.BAL.ILogic
{
    public interface IAccountRepository
    {
        Task<bool> Registration(SignUpModel signUpModel);
        Task<ApplicationUserVModel> Authentication(SignInModel signInModel);
        Task SignOut();
    }
}
