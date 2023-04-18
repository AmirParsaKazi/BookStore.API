using BookStore.Common.Models.Interfaces;
using BookStore.Infrastructure.Data;
using BookStore.Infrastructure.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Busines.Configuration
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDBContext _context;
        private readonly ILogger _logger;
       
        public IAuthorRepository Author { get; private set; }
        public IBookRepository Book { get; private set; }
        public ICategoryRepository Category { get; private set; }
        public ICityRepository City { get; private set; }
        public ICustomerRepository Customer { get; private set; }
        public IDiscountRepository Discount { get; private set; }
        public ILanguageRepository Language { get; private set; }
        public IOrderRepository Order { get; private set; }
        public IOrderStatusRepository OrderStatus { get; private set; }
        public IProvinceRepository Province { get; private set; }

        public UnitOfWork(ApplicationDBContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("logs");

            Author = new AuthorRepository(context, _logger);
            Book = new BookRepository(context, _logger);
            Category = new CategoryRepository(context, _logger);
            City = new CityRepository(context, _logger);
            Customer = new CustomerRepository(context, _logger);
            Discount = new DiscountRepository(context, _logger);
            Language = new LanguageRepository(context, _logger);
            Order = new OrderRepository(context, _logger);
            OrderStatus = new OrderStatusRepository(context, _logger);
            Province = new ProvinceRepository(context, _logger);
        }

        public async Task CompleteAsync()
        {
            try{
                await _context.SaveChangesAsync();
            }
            catch (Exception er)
            {
                _logger.LogError(er, er.Message, typeof(UnitOfWork));
            }
        }

        public async Task Dispose()
        {
            try
            {
                await _context.DisposeAsync();
            }
            catch (Exception er)
            {
                _logger.LogError(er, er.Message, typeof(UnitOfWork));
            }
        }
    }
}
