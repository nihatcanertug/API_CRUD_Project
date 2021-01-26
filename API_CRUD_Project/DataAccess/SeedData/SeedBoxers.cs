using API_CRUD_Project.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_CRUD_Project.DataAccess.SeedData
{
    public class SeedBoxers : IEntityTypeConfiguration<Boxer>
    {
        public void Configure(EntityTypeBuilder<Boxer> builder)
        {
            builder.HasData(
                new Boxer { Id = 1, FullName = "Muhammed Ali", Alias = "King In the Ring", Division = "Heavyweight", Status = "Retired" },
                new Boxer { Id = 2, FullName = "Mike Tyson", Alias = "Iron", Division = "Heavyweight", Status = "Retired" },
                new Boxer { Id = 3, FullName = "Lenox Lewis", Alias = "Lioness", Division = "Heavyweight", Status = "Retired" },
                new Boxer { Id = 4, FullName = "Evander Holyfiled", Alias = "Real Deal", Division = "Heavyweight", Status = "Retired" },
                new Boxer { Id = 5, FullName = "Rock Marciano", Alias = "Savage", Division = "Heavyweight", Status = "Retired" });
        }
    }
}
