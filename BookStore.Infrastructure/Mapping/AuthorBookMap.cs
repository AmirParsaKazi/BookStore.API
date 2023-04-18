using BookStore.Common.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.Mapping;

public class AuthorBookMap : IEntityTypeConfiguration<AuthorBook>
{
    public void Configure(EntityTypeBuilder<AuthorBook> builder)
    {
        builder.HasKey(p => new { p.BookId, p.AuthorId });

        builder.HasOne(p => p.Book)
            .WithMany(q => q.Authors)
            .HasForeignKey(p => p.BookId);

        builder.HasOne(p => p.Author)
            .WithMany(q => q.Books)
            .HasForeignKey(p => p.AuthorId);
    }
}
