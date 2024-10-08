using BookLibraryApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BookLibraryApi.Orm
{
    /// <summary>
    /// Represents the database context for the Book Library API.
    /// </summary>
    public class BookDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BookDbContext"/> class.
        /// </summary>
        /// <param name="options">The options to be used by the DbContext.</param>
        public BookDbContext(DbContextOptions<BookDbContext> options)
            : base(options) { }

        /// <summary>
        /// Gets or sets the DbSet of books.
        /// </summary>
        public DbSet<Book> Books => Set<Book>();
        
        /// <summary>
        /// Seeds the database with initial data.
        /// </summary>
        /// <param name="app">The web application instance.</param>
        public static void SeedDatabase(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<BookDbContext>();

            // Check if the database is empty
            if (!context.Books.Any())
            {
                // Generate a list of books
                var books = Enumerable.Range(1, 100).Select(i => new Book
                {
                    Title = $"Book Title {i}",
                    Author = $"Author for book {i}",
                    Year = 2000 + (i % 20)
                }).ToList();

                // Add the books to the database
                context.Books.AddRange(books);
                context.SaveChanges();
            }
        }
    }
}
