using BookStore.Common.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.Mapping
{
    internal class BookMap : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id)
                .HasColumnType("varchar(450)");

            builder.Property(p => p.Title)
                .HasColumnType("nvarchar(200)");

            builder.Property(p => p.Summary)
               .HasColumnType("nvarchar(max)");

            builder.Property(p => p.Image)
                .HasColumnType("image");

            builder.HasOne(p => p.Language)
                .WithMany(q => q.Books)
                .HasForeignKey(f => f.LanguageId);
        }
    }
}
