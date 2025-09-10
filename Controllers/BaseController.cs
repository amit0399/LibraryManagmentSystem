using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystemMVC_Project.Controllers
{
    public class BaseController : Controller
    {

        private readonly ILogger<BaseController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BaseController(ILogger<BaseController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }


        protected string? CurrentUserId() => HttpContext.Session.GetString("UserId");
        protected string? CurrentUserRole() => HttpContext.Session.GetString("Role");
        protected bool IsAdmin() => string.Equals(CurrentUserRole(), "Admin", StringComparison.OrdinalIgnoreCase);
        protected bool IsUser() => string.Equals(CurrentUserRole(), "User", StringComparison.OrdinalIgnoreCase);
    }
}
