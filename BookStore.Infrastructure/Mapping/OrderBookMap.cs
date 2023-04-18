using BookStore.Common.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.Mapping;

public class OrderBookMap : IEntityTypeConfiguration<OrderBook>
{
    public void Configure(EntityTypeBuilder<OrderBook> builder)
    {
        builder.HasKey(p => new {p.BookId,p.OrderId});

        builder.HasOne(p => p.Book)
            .WithMany(q => q.Orders)
            .HasForeignKey(p => p.BookId);

        builder.HasOne(p => p.Order)
           .WithMany(q => q.Books)
           .HasForeignKey(p => p.OrderId);
    }
}
