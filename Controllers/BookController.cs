using System.Diagnostics.CodeAnalysis;
using LibraryManagementSystemMVC_Project.BAL.ILogic;
using LibraryManagementSystemMVC_Project.Models.RequestModels;
using LibraryManagementSystemMVC_Project.Models.ResponseModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LibraryManagementSystemMVC_Project.Controllers
{
    public class BookController : BaseController
    {

        private readonly ILogger<BookController> _logger;
        private readonly IBookRepository _bookRepository;

        public BookController(ILogger<BookController> logger, IBookRepository bookRepository, IHttpContextAccessor httpContextAccessor, ILogger<BaseController> baseLogger) : base(baseLogger, httpContextAccessor)
        {
            _logger = logger;
            _bookRepository = bookRepository;
        }
        [HttpGet]
        public async Task<IActionResult> CreateBook()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateBook(BookModel bookModel)
        {
            try
            {
                var result=await _bookRepository.CreateBook(bookModel);
            
            }
            catch (Exception ex)
            {
                _logger.LogCritical(DateTime.Now.ToString() + "Error on BookController in CreateBook Action Method : " + ex.Message);
            }
            return RedirectToAction("GetBooksList", "Book");
        }

        [HttpGet]
        public async Task<IActionResult> GetBook(string id)
        {
            BookVModel bookVModel = new BookVModel();
            try
            {
                bookVModel=await _bookRepository.GetBook(id);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(DateTime.Now.ToString() + "Error on Book controller in GetBook Action Method : " + ex.Message);
            }
            return View (bookVModel);
        }


        [HttpGet]
        public async Task<IActionResult> GetBooksList()
        {
            IEnumerable<BookVModel> bookVModel=new List<BookVModel>();
            try
            {
                bookVModel = await _bookRepository.GetBooksList();
            }
            catch (Exception ex)
            {
                _logger.LogCritical(DateTime.Now.ToString() + "Error on BookController in GetBooksList Action Method : " + ex.Message);
            }


            return View(bookVModel);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateBook(string id)
        {
            BookVModel bookVModel = new BookVModel();
            try
            {
                bookVModel = await _bookRepository.GetBook(id);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(DateTime.Now.ToString() + "Error on Book controller in GetBook Action Method : " + ex.Message);
            }
            return View(bookVModel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBook(BookModel bookModel, string id)
        {
            try
            {
                var result = await _bookRepository.UpdateBook(bookModel, id);

            }
            catch (Exception ex)
            {
                _logger.LogCritical(DateTime.Now.ToString() + "Error on Book controller in UpdateBook Action Method" + ex.Message);
            }
            return RedirectToAction("GetBooksList", "Book");
        }

        public async Task<IActionResult> DeleteBook(string id)
        {
            try
            {
                var result =await _bookRepository.DeleteBook(id);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(DateTime.Now.ToString() + "Error on Book Controller in DeleteBook Action Method : " + ex.Message);
            }
            return RedirectToAction("GetBooksList", "Book");
        }

        
    }
}
