using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Http;
using BookLibraryApi.Models;
using BookLibraryApi.Services;
using BookLibraryApi.Orm;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

public class BookApiEndpointsTests
{
    [Fact]
    public async Task AddBook_ReturnsCreatedResult_WhenBookIsAdded()
    {
        // Arrange
        var mockService = new Mock<IBookService<BookDbContext>>();
        var newBook = new Book { Id = 1, Title = "New Book", Author = "New Author", Year = 2021 };

        mockService.Setup(service => service.AddBookAsync(It.IsAny<Book>()))
                   .ReturnsAsync(newBook);

        var builder = WebApplication.CreateBuilder();
        builder.Services.AddSingleton(mockService.Object);
        builder.Services.AddControllers(); // Add this line if you have controllers

        var app = builder.Build();

        app.MapPost("/books", async (Book book, IBookService<BookDbContext> service) =>
        {
            var addedBook = await service.AddBookAsync(book);
            return Results.Created($"/books/{addedBook.Id}", addedBook);
        });

        var server = new TestServer(new WebHostBuilder()
            .UseTestServer()
            .ConfigureServices(services =>
            {
                services.AddSingleton(mockService.Object);
                services.AddControllers(); // Add this line if you have controllers
            })
            .Configure(app =>
            {
                app.UseRouting();
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapPost("/books", async (Book book, IBookService<BookDbContext> service) =>
                    {
                        var addedBook = await service.AddBookAsync(book);
                        return Results.Created($"/books/{addedBook.Id}", addedBook);
                    });
                });
            }));

        var client = server.CreateClient();

        var content = new StringContent(JsonConvert.SerializeObject(newBook), Encoding.UTF8, "application/json");

        // Act
        var response = await client.PostAsync("/books", content);

        // Assert
        Assert.Equal(StatusCodes.Status201Created, (int)response.StatusCode);
    }
}
