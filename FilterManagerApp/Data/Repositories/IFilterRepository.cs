using FilterManagerApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterManagerApp.Data.Repositories
{
    public interface IFilterRepository
    {
        void WriteAllToConsole();
        void AddNewFilter();
        void ModifyFilter();
        void DeleteFilter();
        void SaveFiltersToFile();
        public event EventHandler<Filter>? ItemAdded;
        public event EventHandler<Filter>? ItemRemoved;
        public event EventHandler<Filter>? ItemChanged;
        public event EventHandler? SavedToFile;


    }
}
