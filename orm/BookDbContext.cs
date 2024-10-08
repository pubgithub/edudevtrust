using BookLibraryApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BookLibraryApi.Orm
{
    public class BookDbContext : DbContext
    {
        public BookDbContext(DbContextOptions<BookDbContext> options)
            : base(options) { }

        public DbSet<Book> Books => Set<Book>();
        
        public static void SeedDatabase(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<BookDbContext>();

            if (!context.Books.Any())
            {
                var books = Enumerable.Range(1, 100).Select(i => new Book
                {
                    Title = $"Book Title {i}",
                    Author = $"Author for book {i}",
                    Year = 2000 + (i % 20)
                }).ToList();

                context.Books.AddRange(books);
                context.SaveChanges();
            }
        }
    }

}