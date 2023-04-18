using BookStore.Common.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.Mapping;

public class CustomerMap : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .HasColumnType("varchar(450)");

        builder.Property(p => p.FirstName)
            .HasColumnType("nvarchar(100)");

        builder.Property(p => p.LastName)
            .HasColumnType("nvarchar(100)");

        builder.Property(p => p.Address)
            .HasColumnType("nvarchar(500)");

        builder.Property(p => p.Mobile)
           .HasColumnType("varchar(11)");

        builder.Ignore(p => p.Age);

        builder.HasOne(p => p.City1)
            .WithMany(q => q.Customers1)
            .HasForeignKey(f => f.CityId1);

        builder.HasOne(p => p.City2)
           .WithMany(q => q.Customers2)
           .HasForeignKey(f => f.CityId2);
    }
}
