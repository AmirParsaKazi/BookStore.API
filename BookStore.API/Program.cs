using BookStore.Busines;
using BookStore.Busines.Configuration;
using BookStore.Busines.Services;
using BookStore.Common.Models.Interfaces.IServices;
using BookStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


//* Add DbContext to Database to push changes to database
builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration
        .GetConnectionString("DefaultConnection"));
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//add DtoEntityMapperProfile class as AutoMapper 
builder.Services.AddAutoMapper(typeof(DtoEntityMapperProfile));

//DI Unit Of Work
builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<ILanguageService, LanguageService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IDiscountService, DiscountService>();
builder.Services.AddScoped<IProvinceService, ProvinceService>();
builder.Services.AddScoped<ICityService, CityService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IOrderStatusService, OrderStatusService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
