using BookStore.Common.Models;
using BookStore.Infrastructure.Mapping;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.Data;

//add-migration -name init -context ApplicationDBContext -o Data/Migrations
//update-database
//remove-migration
public class ApplicationDBContext : DbContext
{
    public ApplicationDBContext(DbContextOptions options) : base(options){}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AuthorMap());
        modelBuilder.ApplyConfiguration(new BookMap());
        modelBuilder.ApplyConfiguration(new AuthorBookMap());
        modelBuilder.ApplyConfiguration(new CategoryMap());
        modelBuilder.ApplyConfiguration(new CategoryBookMap());
        modelBuilder.ApplyConfiguration(new ProvinceMap());
        modelBuilder.ApplyConfiguration(new CityMap());
        modelBuilder.ApplyConfiguration(new CustomerMap());
        modelBuilder.ApplyConfiguration(new DiscountMap());
        modelBuilder.ApplyConfiguration(new LanguageMap());
        modelBuilder.ApplyConfiguration(new OrderBookMap());
        modelBuilder.ApplyConfiguration(new OrderMap());
        modelBuilder.ApplyConfiguration(new OrderStatusMap());
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Discount> Discounts { get; set; }
    public DbSet<Language> Languages { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderStatus> OrderStatuses { get; set; }
    public DbSet<Province> Provinces { get; set; }
    public DbSet<OrderBook> OrderBooks { get; set; }
    public DbSet<AuthorBook> AuthorBooks { get; set; }
    public DbSet<CategoryBook> CategoryBooks { get; set; }


}