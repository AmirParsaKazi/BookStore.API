using BookStore.Common.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.Mapping;

public class LanguageMap : IEntityTypeConfiguration<Language>
{
    void IEntityTypeConfiguration<Language>.Configure(EntityTypeBuilder<Language> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .HasColumnType("varchar(450)");

        builder.Property(p => p.Name)
            .HasColumnType("nvarchar(150)");
    }
}
