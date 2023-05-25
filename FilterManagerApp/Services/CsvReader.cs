using FilterManagerApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterManagerApp.Services
{
    public class CsvReader<T>: ICsvReader<T>
    {
        public List<Filter> ProcessFilters()
        {
            string filePath = @"Resources\Files\filtr-catalogue.csv";

            if (!File.Exists(filePath))
            {
                return new List<Filter>();
            }

            var filters = File.ReadAllLines(filePath)
                .Skip(1)
                .Where(x => x.Length > 1)
                .Select(x =>
                {
                    var columns = x.Split(',');
                    return new Filter()
                    {
                        Id = int.Parse(columns[0]),
                        Name = columns[1],
                        Type = columns[2],
                        Currency = columns[3],
                        NetPrice = decimal.Parse(columns[4], CultureInfo.InvariantCulture),
                        GrossPrice = decimal.Parse(columns[5], CultureInfo.InvariantCulture)
                    };
                });

            return filters.ToList();
        }
    }
}
