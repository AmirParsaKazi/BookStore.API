using BookStore.Common.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.Mapping;

public class DiscountMap : IEntityTypeConfiguration<Discount>
{
    public void Configure(EntityTypeBuilder<Discount> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id).HasColumnName("BookId");

        builder.HasOne(p => p.Book)
            .WithOne(p => p.Discount)
            .HasForeignKey<Discount>(f => f.Id);
    }
}
