using LibraryManagementSystemMVC_Project.BAL.ILogic;
using LibraryManagementSystemMVC_Project.DatabaseConnection;
using LibraryManagementSystemMVC_Project.Models.EntityModels;
using LibraryManagementSystemMVC_Project.Models.RequestModels;
using LibraryManagementSystemMVC_Project.Models.ResponseModels;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystemMVC_Project.BAL.Logic
{
    public class BookRepository : IBookRepository
    {

        private readonly ILogger<BookRepository> _logger;
        private readonly DBConnectionFactory _dbConnectionFactory;

        public BookRepository(ILogger<BookRepository> logger, DBConnectionFactory dBConnectionFactory)
        { 
            _logger= logger;
            _dbConnectionFactory= dBConnectionFactory;
        }



        public async Task<int> CreateBook(BookModel bookModel)
        {
            
            try
            {
                if (bookModel != null)
                {
                    Book book = new Book()
                    {
                        BookId = Guid.NewGuid(),
                        Title = bookModel.Title,
                        Author = bookModel.Author,
                        Category = bookModel.Category,
                        Quantity = bookModel.Quantity,
                        CreatedOn = DateTime.Now,
                        CreatedBy = "Admin",
                        IsDeleted = false

                    };
                    await _dbConnectionFactory.tblBooks.AddAsync(book);
                    await _dbConnectionFactory.SaveChangesAsync();
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical(DateTime.Now.ToString() + "Error on BookRepository in Createbook Method : " + ex.Message);
            }
            return 1;
        }

        public async Task<int> DeleteBook(string bookId)
        {
            try
            {
                if (!string.IsNullOrEmpty(bookId))
                {
                    var result=await _dbConnectionFactory.tblBooks.Where(option=>option.BookId==Guid.Parse(bookId)).FirstOrDefaultAsync();
                    if (result != null)
                    {
                        result.DeletedOn = DateTime.Now;
                        result.DeletedBy = "Admin";
                        result.IsDeleted = true;


                        await _dbConnectionFactory.SaveChangesAsync();
                        return 1;
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
                _logger.LogCritical(DateTime.Now.ToString() + "Error On Book Repositroy in DeleteBook Method : " + ex.Message);
                return 0;
            }
            return 1;
        }

        public async Task<IEnumerable<BookVModel>> GetAllBooks()
        {
            IEnumerable<BookVModel> bookVModel = new List<BookVModel>();
            try
            {
                bookVModel = await _dbConnectionFactory.tblBooks.Where(option => option.IsDeleted == false).Select(option => new BookVModel()
                {
                    BookId=option.BookId,
                    Title=option.Title,

                }).ToListAsync();     
            }
            catch(Exception ex)
            {

                _logger.LogCritical(DateTime.Now.ToString()+ "Error on Repository : " +ex.Message);
            }
            return bookVModel;


        }

        public async Task<BookVModel> GetBook(string bookId)
        {
             BookVModel bookVModel=new BookVModel();
            try
            {
                if (!string.IsNullOrEmpty(bookId))
                {
                    bookVModel = await _dbConnectionFactory.tblBooks.Where(option => option.BookId == Guid.Parse(bookId)).Select(option => new BookVModel()
                    {
                        BookId = option.BookId,
                        Title = option.Title,
                        Author = option.Author,
                        Category = option.Category,
                        Quantity = option.Quantity,
                    }).FirstOrDefaultAsync();
                }
                else
                {
                    
                }
                

            }
            catch (Exception ex)
            {
                _logger.LogCritical(DateTime.Now.ToString() + "Error on BookRepository in GetBooksList Method  " + ex.Message);
            }
            return bookVModel;

        }

        public async Task<IEnumerable<BookVModel>> GetBooksList()
        {
            IList<BookVModel> bookVModel= new List<BookVModel>();
            try
            {
                bookVModel =await _dbConnectionFactory.tblBooks.Where(option => option.IsDeleted == false).Select(option => new BookVModel()
                {
                    BookId = option.BookId,
                    Title = option.Title,
                    Author = option.Author,
                    Category = option.Category,
                    Quantity = option.Quantity,
                }).ToListAsync();

            }
            catch (Exception ex)
            {
                _logger.LogCritical(DateTime.Now.ToString() + "Error on BookRepository in GetBooksList Method  " + ex.Message);
            }
            return bookVModel;
        }

        public async Task<int> UpdateBook(BookModel bookModel,string bookId)
        {
            try
            {
                var result =await _dbConnectionFactory.tblBooks.Where(option => option.BookId == Guid.Parse(bookId)).FirstOrDefaultAsync();
                if (result != null)
                {
                    result.Title = bookModel.Title;
                    result.Author= bookModel.Author;
                    result.Category= bookModel.Category;
                    result.Quantity = bookModel.Quantity;
                    result.UpdatedOn = DateTime.Now;
                    result.UpdatedBy = "Admin";


                    _dbConnectionFactory.tblBooks.Update(result);
                    await _dbConnectionFactory.SaveChangesAsync();

                }
                else
                {
                    return 0;
                }
                
                
                    
                
            }
            catch (Exception ex)
            {
                _logger.LogCritical(DateTime.Now.ToString() + "Error on BookRepository in Createbook Method : " + ex.Message);
                return 0;
            }
            return 1;
        }
    }
}
