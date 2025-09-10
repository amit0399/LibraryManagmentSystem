using LibraryManagementSystemMVC_Project.BAL.ILogic;
using LibraryManagementSystemMVC_Project.BAL.Logic;
using LibraryManagementSystemMVC_Project.Models.ResponseModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystemMVC_Project.Controllers
{
    public class ApplicationUserController : BaseController
    {
        private readonly ILogger<ApplicationUserController> _logger;
        private readonly IApplicationUserRepository _applicationUserRepository;

        public ApplicationUserController(ILogger<ApplicationUserController> logger, IApplicationUserRepository applicationUserRepository, IHttpContextAccessor httpContextAccessor, ILogger<BaseController> baseLogger) : base(baseLogger, httpContextAccessor)
        {
            _logger=logger;
            _applicationUserRepository = applicationUserRepository;
        }

        public async Task<IActionResult> GetApplicationUserList()
        {
            IEnumerable<ApplicationUserVModel> applicationUserVModel = new List<ApplicationUserVModel>();
            try
            {
                applicationUserVModel = await _applicationUserRepository.GetApplicationUserList();


            }
            catch (Exception ex)
            {
                _logger.LogCritical(DateTime.Now.ToString() + "Error on UserMember Controller in GetUserMemberList Action Method : " + ex.Message);
            }
            return View(applicationUserVModel);
        }

        public async Task<IActionResult> GetApplicationUserDetails(string userId)
        {
            ApplicationUserVModel applicationUserVModel = new ApplicationUserVModel();
            try
            {
                applicationUserVModel = await _applicationUserRepository.GetApplicationUserDeatils(userId);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(DateTime.Now.ToString() + "Error on Controller" + ex.Message);
            }
            return View(applicationUserVModel);
            
        }

        public async Task<IActionResult> DeleteApplicationUser(string userId)
        {
            try
            {
                var result = await _applicationUserRepository.DeleteApplicationUser(userId);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(DateTime.Now.ToString() + "Error on controller " + ex.Message);
            }
            return RedirectToAction("GetApplicationUserList", "ApplicationUser");
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
