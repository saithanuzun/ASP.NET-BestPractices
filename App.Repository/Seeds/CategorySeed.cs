using System;
using App.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Repository.Seeds
{
    public class CategorySeed : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData
            (
                new Category(){Id=1, Name="kalemler",},
                new Category(){Id=2, Name="kalemler2",},
                new Category(){Id=3, Name="kalemler3",}
            );
        }
    }
}

