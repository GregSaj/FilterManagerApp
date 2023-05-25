using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterManagerApp.Data.Entities
{
    public class FilterManagerAppDbContext: DbContext
    {
        private readonly string _connectionString = @"Data Source=DESKTOP-0D8TD9S;Database=FilterManagerDb;Integrated Security=True;TrustServerCertificate=True";
        public DbSet<Filter> Filters => Set<Filter>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {               
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
