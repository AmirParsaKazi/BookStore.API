using BookStore.Common.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.Mapping;

public class AuthorMap : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .HasColumnType("varchar(450)");

        builder.Property(p => p.FirstName)
            .HasColumnType("nvarchar(200)");

        builder.Property(p => p.LastName)
           .HasColumnType("nvarchar(200)");
    }
}
