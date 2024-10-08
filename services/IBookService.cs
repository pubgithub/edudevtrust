using BookLibraryApi.Models;

namespace BookLibraryApi.Services
{
    public interface IBookService<DBContext>
    {
        //GetBooks
        IQueryable<IBook> GetAllBooks();

        //SearchBooks
        IQueryable<IBook> SearchBooks(string? title, string? author);

        //GetBooksById
        Task<IBook?> GetBookByIdAsync(int id);

        //AddBook
        Task<IBook> AddBookAsync(Book book);

        //UpdateBook
        Task<bool> UpdateBookAsync(int id, IBook updatedBook);

        //DeleteBook
        Task<IBook?> DeleteBookAsync(int id);
    }
}
