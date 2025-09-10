using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using LibraryManagementSystemMVC_Project.BAL.ILogic;
using LibraryManagementSystemMVC_Project.BAL.Logic;
using LibraryManagementSystemMVC_Project.DatabaseConnection;
using LibraryManagementSystemMVC_Project.Models.RequestModels;
using LibraryManagementSystemMVC_Project.Models.ResponseModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LibraryManagementSystemMVC_Project.Controllers
{
    public class IssueController : BaseController
    {
        private readonly ILogger<IssueController> _logger;
        private readonly IIssueRepository _issueRepository;
        private readonly IApplicationUserRepository _applicationUserRepository;
        private readonly IBookRepository _bookRepository;
        private readonly DBConnectionFactory _dBConnectionFactory;

        public IssueController(ILogger<IssueController> logger, IIssueRepository issueRepository, IApplicationUserRepository applicationUserRepository, IBookRepository bookRepository, DBConnectionFactory dBConnectionFactory, IHttpContextAccessor httpContextAccessor, ILogger<BaseController> baseLogger) : base(baseLogger, httpContextAccessor)
        {
            _logger = logger;
            _issueRepository = issueRepository;
            _applicationUserRepository = applicationUserRepository;
            _bookRepository = bookRepository;
            _dBConnectionFactory = dBConnectionFactory;
        }

        [HttpGet]
        public async Task<IActionResult> CreateIssueBook()
            {
            var members = await _applicationUserRepository.GetAllUserMembers();
            var books = await _bookRepository.GetAllBooks();

            ViewBag.members = new SelectList(members, "UserId", "UserName");
            ViewBag.books = new SelectList(books, "BookId", "Title");


            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateIssueBook(IssueModel issueModel)
        {
            try
            {
                var result=await _issueRepository.CreateIssueBook(issueModel);

            }
            catch (Exception ex)
            {
                _logger.LogCritical(DateTime.Now.ToString() + "Error on Controller" + ex.Message);
            }
            return RedirectToAction("GetIssueBookList", "Issue");
        }

        [HttpGet]
        public async Task<IActionResult> GetIssueBook(string id)
        {
            IssueVModel issueVModel = new IssueVModel();
            try
            {
                issueVModel=await _issueRepository.GetIssueBook(id);
            }
            catch (Exception ex) 
            {
                _logger.LogCritical(DateTime.Now.ToString()+"Error on Controller : "+ex.Message);
                    
            }
            return View(issueVModel);
        }



        [HttpGet]
        public async Task<IActionResult> GetIssueBookList()
        {
            IEnumerable<IssueVModel> issueVModel = new List<IssueVModel>();
            try
            {
                issueVModel = await _issueRepository.GetIssueBookList();
            }
            catch(Exception ex)
            {
                _logger.LogCritical(DateTime.Now.ToString() + "Error on COntroller : " + ex.Message);
            }
            return View(issueVModel);
        }
        [HttpGet]
        public async Task<IActionResult> UpdateIssueBook(string id)
        {
            IssueVModel issueVModel = new IssueVModel();
            try
            {
                issueVModel = await _issueRepository.GetIssueBook(id);

                ViewBag.books = new SelectList(_dBConnectionFactory.tblBooks.Where(option=>option.IsDeleted==false).ToList(), "BookId", "Title", issueVModel.BookId);
                ViewBag.members = new SelectList(_dBConnectionFactory.tblApplicationUsers.Where(option=>option.IsDeleted==false).ToList(), "UserId", "UserName", issueVModel.UserId);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(DateTime.Now.ToString() + "Error on Controller : " + ex.Message);

            }
            return View(issueVModel);
            
        }

        [HttpPost]
        public async Task<IActionResult> UpdateIssueBook(IssueModel issueModel ,string id)
        {
            try
            {
                var result = await _issueRepository.UpdateIssueBook(issueModel, id);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(DateTime.Now.ToString() + "Error on Controller : " + ex.Message);
            }
            return RedirectToAction("GetIssueBookList","Issue");
        }
        [HttpGet]
        public async Task<IActionResult> DeleteIssueBook(string id)
        {
            try
            {
                var result=await _issueRepository.DeleteIssueBook(id);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(DateTime.Now.ToString() + "Error on Controller : " + ex.Message);
            }
            return RedirectToAction("GetIssueBookList","Issue");
        }

        

        [HttpGet]
        public async Task<IActionResult> ConfirmBookReturn(string id)
        {
            try
            {
                var result = await _issueRepository.ConfirmBookReturn(id);
                if (result == 1)
                {
                    TempData["msg"] = "Book Successfully Return";
                }
                else
                {
                    TempData["msg"] = "Error in returning Book ";
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical(DateTime.Now.ToString() + "Error on Issue Controller in ConfirmbookReturn MAction Method : " + ex.Message);
            }
            return RedirectToAction("GetIssueBookList", "Issue");
        }

    }
}
