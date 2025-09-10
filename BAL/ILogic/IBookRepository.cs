using LibraryManagementSystemMVC_Project.Models.RequestModels;
using LibraryManagementSystemMVC_Project.Models.ResponseModels;

namespace LibraryManagementSystemMVC_Project.BAL.ILogic
{
    public interface IBookRepository
    {
        Task<IEnumerable<BookVModel>> GetBooksList();

        Task<BookVModel> GetBook(string bookId);
        Task<int> CreateBook(BookModel bookModel);

        Task<int> UpdateBook(BookModel bookModel,string bookid);

        Task<int> DeleteBook(string bookId);
        Task<IEnumerable<BookVModel>> GetAllBooks();


    }
}
