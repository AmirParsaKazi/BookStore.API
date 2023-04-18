using BookStore.Common.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.Mapping;

public class CategoryBookMap : IEntityTypeConfiguration<CategoryBook>
{
    public void Configure(EntityTypeBuilder<CategoryBook> builder)
    {
        builder.HasKey(p => new {p.CategoryId,p.BookId});

        builder.HasOne(p => p.Book)
            .WithMany(q => q.Categories)
            .HasForeignKey(f => f.BookId).OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(p => p.Category)
           .WithMany(q => q.Books)
           .HasForeignKey(f => f.CategoryId).OnDelete(DeleteBehavior.Cascade);
    }
}
