using FilterManagerApp.Data.Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterManagerApp.Services
{
    public class FilterGenerator : IFilterGenerator
    {
        private readonly ICsvReader<Filter> _csvReader;
        private readonly FilterManagerAppDbContext _filterManagerAppDbContext;

        public FilterGenerator(ICsvReader<Filter> csvReader, FilterManagerAppDbContext filterManagerAppDbContext)
        {
            _csvReader = csvReader;
            _filterManagerAppDbContext = filterManagerAppDbContext;
        }

        public List<Filter> LoadFiltersFromCsv()
        {
            var filters = _csvReader.ProcessFilters();
            return filters;
        }

        public void SendFiltersToSql()
        {
            if (!CheckDatabaseExists()) //tutaj skończyłem
            {
                _filterManagerAppDbContext.Database.EnsureCreated();

                var filters = LoadFiltersFromCsv();

                foreach (var filter in filters)
                {
                    _filterManagerAppDbContext.Filters.Add(new Filter()
                    {
                        Name = filter.Name,
                        Type = filter.Type,
                        Currency = filter.Currency,
                        NetPrice = filter.NetPrice,
                        GrossPrice = filter.GrossPrice
                    });

                    _filterManagerAppDbContext.SaveChanges();
                }
            }
        }     

        private static bool CheckDatabaseExists()
        {
            string sqlQuery;
            string connectionString = @"Data Source=DESKTOP-0D8TD9S;Database=FilterManagerDb;Integrated Security=True;TrustServerCertificate=True";
            string databaseName = "FilterManagerDb";
            bool result = false;
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                sqlQuery = string.Format("SELECT database_id FROM sys.databases WHERE Name = '{0}'", databaseName);
                using (conn)
                {
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
                    {
                        conn.Open();
                        int databaseID = (int)cmd.ExecuteScalar();
                        conn.Close();
                        result = (databaseID > 0);
                    }
                }
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }

    }
}
