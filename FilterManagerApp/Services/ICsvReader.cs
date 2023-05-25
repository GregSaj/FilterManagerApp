using FilterManagerApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterManagerApp.Services
{
    public interface ICsvReader<T>
    {
        List<Filter> ProcessFilters();
    }
}
