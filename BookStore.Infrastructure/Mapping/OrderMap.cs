using BookStore.Common.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.Mapping;

public class OrderMap : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .HasColumnType("varchar(450)");

        builder.HasOne(p => p.Staus)
            .WithMany(q => q.Orders)
            .HasForeignKey(f => f.OrderStausId);

        builder.HasOne(p => p.Customer)
            .WithMany(q => q.Orders)
            .HasForeignKey(f => f.CustomerId);
    }
}
