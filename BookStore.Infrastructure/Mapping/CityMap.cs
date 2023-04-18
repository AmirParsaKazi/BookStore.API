using BookStore.Common.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.Mapping;

public class CityMap : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .HasColumnType("varchar(450)");

        builder.Property(p => p.Name)
            .HasColumnType("nvarchar(150)");

        builder.HasOne(p => p.Province)
            .WithMany(q => q.Cities)
            .HasForeignKey(f => f.ProvinceId);
    }
}
