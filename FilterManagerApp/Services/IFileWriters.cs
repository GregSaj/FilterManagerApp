using FilterManagerApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterManagerApp.Services
{
    public interface IFileWriters<T> where T : class
    {
        void SaveToJson(List<T> list);
        void SaveToCsv(List<T> list);
        void SaveToXml(List<Filter> list);
    }
}
