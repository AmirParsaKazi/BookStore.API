using BookStore.Common.Dtos.Book;
using BookStore.Common.Dtos.Customer;
using BookStore.Common.Dtos.OrderStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Common.Dtos.Order
{
    public class OrderGet
    {
        public OrderGet(int TotalPurchasePrice, DateTime RegistrationDate, string? DispatchNumber)
        {
            this.TotalPurchasePrice = TotalPurchasePrice;
            this.RegistrationDate = RegistrationDate;
            this.DispatchNumber = DispatchNumber;
        }

        public int TotalPurchasePrice { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string? DispatchNumber { get; set; }
        public OrderStatusesList Staus { get; set; }
        public CustomersList Customer { get; set; }
        public IEnumerable<BooksList> Books { get; set; }
    }
}
