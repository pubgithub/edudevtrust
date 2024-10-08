using BookLibraryApi.Models;
using BookLibraryApi.Orm;
using BookLibraryApi.Services;
using Microsoft.AspNetCore.OData.Query;


/// <summary>
/// Maps the book-related endpoints to the application.
/// </summary>
/// <param name="app">The endpoint route builder.</param>
public static class BookApiEndpoints
{
    public static void MapBookEndpoints(this IEndpointRouteBuilder app)
    {
        // Endpoint to display a welcome message
        app.MapGet("/", () => "Welcome to the Education Trust Booking Library API!");

        // Endpoint to get all books
        app.MapGet("/books", (IBookService<BookDbContext> service) =>
            service.GetAllBooks());

        // Endpoint to get a book by its ID
        app.MapGet("/books/{id}", async (int id, IBookService<BookDbContext> service) => 
        {
            var book =  await service.GetBookByIdAsync(id);
            return book is not null 
                ? Results.Ok(book)
                : Results.NotFound();
        });


        // Endpoint to search for books with OData query support
        app.MapGet("/odata/books/search", [EnableQuery] (string? title, string? author, IBookService<BookDbContext> service) =>
        {
            var books = service.SearchBooks(title, author);

            if (!string.IsNullOrEmpty(title))
            {
                books = books.Where(b => b.Title.Contains(title));
            }

            if (!string.IsNullOrEmpty(author))
            {
                books = books.Where(predicate: b => b.Author.Contains(author));
            }

            return books;
        });


        // Endpoint to add a new book
        app.MapPost("/books", async (Book book, IBookService<BookDbContext> service) =>
        {
            await service.AddBookAsync(book);
            return Results.Created($"/books/{book.Id}", book);
        });



        // Endpoint to delete a book by its ID
        app.MapDelete("/books/{id}", async (int id, IBookService<BookDbContext> service) =>
        {
            if (await service.DeleteBookAsync(id) is IBook book)
            {
                return Results.Ok(book);
            }

            return Results.NotFound();
        });
    }
}
