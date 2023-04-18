using BookStore.Common.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.Mapping;

public class CategoryMap : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .HasColumnType("varchar(450)");

        builder.Property(p => p.Title)
            .HasColumnType("nvarchar(150)");

        builder.HasOne(p => p.ParentCategory)
            .WithMany(p => p.Categories)
            .HasForeignKey(f => f.ParentId).OnDelete(DeleteBehavior.Restrict);

    }
}
