using System.Runtime.CompilerServices;
using LibraryManagementSystemMVC_Project.BAL.ILogic;
using LibraryManagementSystemMVC_Project.Models.RequestModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace LibraryManagementSystemMVC_Project.Controllers
{
    public class AccountController : BaseController
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountRepository _accountRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountController(ILogger<AccountController> logger, IAccountRepository accountRepository,IHttpContextAccessor httpContextAccessor,ILogger<BaseController> baseLogger):base(baseLogger,httpContextAccessor)
        {
                _logger = logger;
            _accountRepository = accountRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> AccessDenied()
        {
            return View();
        }

        public async Task<IActionResult> Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(SignUpModel signUpModel)
        {
            try
            {
                if (signUpModel != null)
                {
                    var result = await _accountRepository.Registration(signUpModel);
                    if (result == false)
                    {
                        return RedirectToAction("Login", "Account");
                        ViewBag.Error = "User Already Exist";
                    }
                    

                }
                else
                {
                    ViewBag.Error = "Model is null or Empty";
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical(DateTime.Now.ToString() + "Error on Account Controller in Register Action Method : " + ex.Message);
            }

            return RedirectToAction("Login", "Account");
                 
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(SignInModel signInModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userResult=await _accountRepository.Authentication(signInModel);

                    if (userResult != null)
                    {
                        HttpContext.Session.SetString("UserId", userResult.UserId.ToString());
                        HttpContext.Session.SetString("Email", userResult.Email);
                        HttpContext.Session.SetString("Role", userResult.Role);

                        if (userResult.Role == "Admin")
                        {   
                            return RedirectToAction("AdminDashboard", "Dashboard");
                        }
                        else
                        {

                            return RedirectToAction("UserDashboard", "Dashboard");
                        }
                    }
                    else
                    {
                        ViewBag.Error = "Invailid UserName OR Password";
                        return View(signInModel);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical(DateTime.Now.ToString() + "Error on Account Controller in Authentication method : " + ex.Message);
            }
            return View(signInModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            _httpContextAccessor.HttpContext.Session.Clear(); // sab session clear
            return RedirectToAction("Login", "Account");
        }

        public IActionResult Index()
        {
            return View();
        }
    }


    
}
