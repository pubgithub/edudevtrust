using Microsoft.EntityFrameworkCore;
using BookLibraryApi;
using BookLibraryApi.Models;
using Microsoft.AspNetCore.OData;
using Microsoft.OData.ModelBuilder;
using Microsoft.AspNetCore.OData.Query;
using BookLibraryApi.Orm;
using BookLibraryApi.Services;

try
{

    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container. DI

    //Enabling In Memory Database support
    builder.Services.AddDbContext<BookDbContext>(options =>
        options.UseInMemoryDatabase("BookLibrary"));

    builder.Services.AddScoped<IBookService<BookDbContext>, BookService>();

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
    app.MapBookEndpoints();
    app.Run();

}
catch {
    
}