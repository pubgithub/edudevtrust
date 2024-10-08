using Microsoft.EntityFrameworkCore;
using BookLibraryApi;
using BookLibraryApi.Models;
using Microsoft.AspNetCore.OData;
using Microsoft.OData.ModelBuilder;
using Microsoft.AspNetCore.OData.Query;
using BookLibraryApi.Orm;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Enabling In Memory Database support
builder.Services.AddDbContext<BookDbContext>(options =>
    options.UseInMemoryDatabase("BookLibrary"));

//Enabling OData queries support
builder.Services.AddControllers().AddOData(opt =>
    opt.AddRouteComponents("odata", OdataConfig.GetEdmModel()).Select().Filter().OrderBy());


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

//Pre-populate this database with some data.
BookDbContext.SeedDatabase(app);


// Configure the Swagger HTTP request pipeline Swagger UI.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/", () => "Welcome to the Book Library API!");

app.MapGet("/books", async (BookDbContext db) =>
    await db.Books.ToListAsync());

app.MapGet("/books/{id}", async (int id, BookDbContext db) =>
    await db.Books.FindAsync(id) is Book book
        ? Results.Ok(book)
        : Results.NotFound());

app.MapGet("/odata/books/search", [EnableQuery] (string? title, string? author, BookDbContext db) =>
{
    var books = db.Books.AsQueryable();

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

app.MapPost("/books", async (Book book, BookDbContext db) =>
{
    db.Books.Add(book);
    await db.SaveChangesAsync();
    return Results.Created($"/books/{book.Id}", book);
});

app.MapPut("/books/{id}", async (int id, Book inputBook, BookDbContext db) =>
{
    var book = await db.Books.FindAsync(id);

    if (book is null) return Results.NotFound();

    book.Title = inputBook.Title;
    book.Author = inputBook.Author;
    book.Year = inputBook.Year;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("/books/{id}", async (int id, BookDbContext db) =>
{
    if (await db.Books.FindAsync(id) is Book book)
    {
        db.Books.Remove(book);
        await db.SaveChangesAsync();
        return Results.Ok(book);
    }

    return Results.NotFound();
});

app.Run();

