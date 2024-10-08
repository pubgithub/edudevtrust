using Microsoft.EntityFrameworkCore;
using BookLibraryApi.Models;
using BookLibraryApi.Orm;

namespace BookLibraryApi.Services
{
    /// <summary>
    /// Provides services for managing books in the library.
    /// </summary>
    public class BookService : IBookService<BookDbContext>
    {
        private readonly BookDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookService"/> class.
        /// </summary>
        /// <param name="context">The database context to be used by the service.</param>
        public BookService(BookDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds a new book to the library.
        /// </summary>
        /// <param name="book">The book to be added.</param>
        /// <returns>The added book.</returns>
        public async Task<IBook> AddBookAsync(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return book;
        }

        /// <summary>
        /// Deletes a book from the library by its ID.
        /// </summary>
        /// <param name="id">The ID of the book to be deleted.</param>
        /// <returns>The deleted book, or null if not found.</returns>
        public async Task<IBook?> DeleteBookAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return null;
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return book;
        }

        /// <summary>
        /// Gets all books in the library.
        /// </summary>
        /// <returns>An IQueryable of all books.</returns>
        public IQueryable<IBook> GetAllBooks()
        {
            return _context.Books.AsQueryable();
        }

        /// <summary>
        /// Gets a book by its ID.
        /// </summary>
        /// <param name="id">The ID of the book to be retrieved.</param>
        /// <returns>The book, or null if not found.</returns>
        public async Task<IBook?> GetBookByIdAsync(int id)
        {
            return await _context.Books.FindAsync(id);
        }

        /// <summary>
        /// Searches for books by title and/or author.
        /// </summary>
        /// <param name="title">The title to search for.</param>
        /// <param name="author">The author to search for.</param>
        /// <returns>An IQueryable of books that match the search criteria.</returns>
        public IQueryable<IBook> SearchBooks(string? title, string? author)
        {
            var books = _context.Books.AsQueryable();

            if (!string.IsNullOrEmpty(title))
            {
                books = books.Where(b => b.Title.Contains(title));
            }

            if (!string.IsNullOrEmpty(author))
            {
                books = books.Where(b => b.Author.Contains(author));
            }

            return books;
        }

        /// <summary>
        /// Updates an existing book by its ID.
        /// </summary>
        /// <param name="id">The ID of the book to be updated.</param>
        /// <param name="updatedBook">The updated book details.</param>
        /// <returns>True if the update was successful, otherwise false.</returns>
        public async Task<bool> UpdateBookAsync(int id, IBook updatedBook)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return false;
            }

            book.Title = updatedBook.Title;
            book.Author = updatedBook.Author;
            book.Year = updatedBook.Year;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
