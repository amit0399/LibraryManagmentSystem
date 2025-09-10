using System.Net.WebSockets;
using LibraryManagementSystemMVC_Project.BAL.ILogic;
using LibraryManagementSystemMVC_Project.BAL.Logic;
using LibraryManagementSystemMVC_Project.Models.ResponseModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystemMVC_Project.Controllers
{
    public class DashboardController : BaseController
    {
        private readonly ILogger<DashboardController> _logger;
        private readonly IDashboardRepository _dashboardRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IIssueRepository _issueRepository;
        private readonly IApplicationUserRepository _applicationUserRepository;

        public DashboardController(
            ILogger<DashboardController> logger,
            IDashboardRepository dashboardRepository,
            IBookRepository bookRepository,
            IIssueRepository issueRepository,
            IApplicationUserRepository applicationUserRepository,
            IHttpContextAccessor httpContextAccessor,
            ILogger<BaseController> baseLogger) : base(baseLogger, httpContextAccessor)
        { 
            _logger= logger;
            _dashboardRepository= dashboardRepository;
            _bookRepository= bookRepository;
            _issueRepository= issueRepository;
            _applicationUserRepository = applicationUserRepository;
            
        }

        //Entry Decide Based on role
        public IActionResult Index()
        {
            if (IsAdmin()) return RedirectToAction("AdminDashboard");
            if (IsUser()) return RedirectToAction("UserDashBoard");
            return RedirectToAction("Login","Account");
        }

        public async Task<IActionResult> AdminDashboard()
        {
            DashboardVModel dashboardVModel=new DashboardVModel();
            try
            {
                var role = HttpContext.Session.GetString("Role");
                if (role != "Admin")
                {
                    return RedirectToAction("AccessDenied", "Account");
                }
                
               dashboardVModel = await _dashboardRepository.AdminDashboard();
                
                
            }
            catch (Exception ex) 
            {
                _logger.LogCritical(DateTime.Now.ToString() + "Error on Dashboard Controller : " + ex.Message);
            }
            return View(dashboardVModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetBookInventoryStatus()
        {
            var role = HttpContext.Session.GetString("Role");
            if (role != "Admin")
            {
                return RedirectToAction("AccessDenied", "Account");
            }
        
             var result = await _dashboardRepository.GetBookInventory();
             return View(result);
   
        }

        [HttpGet]
        public async Task<IActionResult> UserDashboard()
        {
            IList<UserDashboardVModel> userDashboardVModels=new List<UserDashboardVModel>();
            try
            {
                var role = HttpContext.Session.GetString("Role");
                if (role == "Admin")
                {
                    return RedirectToAction("AdminDashboard");
                }

                var userId=HttpContext.Session.GetString("UserId");
                userDashboardVModels = await _dashboardRepository.UserDashboard(userId);
                
            }
            catch (Exception ex)
            {
                _logger.LogCritical(DateTime.Now.ToString() + "Error on Controller UsserDashboard Action method : " + ex.Message);
            }
            return View(userDashboardVModels);
        }


    }
}
