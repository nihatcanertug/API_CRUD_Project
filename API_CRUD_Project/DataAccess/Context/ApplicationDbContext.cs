using API_CRUD_Project.DataAccess.SeedData;
using API_CRUD_Project.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_CRUD_Project.DataAccess.Context
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options){}
        public DbSet<Boxer>Boxers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SeedBoxers());
            base.OnModelCreating(modelBuilder);
        }
    }

    
}
