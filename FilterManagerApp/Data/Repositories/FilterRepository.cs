using FilterManagerApp.Data.Entities;
using FilterManagerApp.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace FilterManagerApp.Data.Repositories
{
    public class FilterRepository : IFilterRepository
    {
        private readonly FilterManagerAppDbContext _filterManagerAppDbContext;
        private readonly IFilterGenerator _filterGenerator;
        private readonly IFileWriters<Filter> _fileWriters;


        public FilterRepository(
            FilterManagerAppDbContext filterManagerAppDbContext,
            IFilterGenerator filterGenerator,
            IFileWriters<Filter> fileWriters
            )
        {
            _filterManagerAppDbContext = filterManagerAppDbContext;
            _filterGenerator = filterGenerator;
            _fileWriters = fileWriters;

        }

        public event EventHandler<Filter>? ItemAdded;
        public event EventHandler<Filter>? ItemRemoved;
        public event EventHandler<Filter>? ItemChanged;
        public event EventHandler? SavedToFile;

        public void WriteAllToConsole()
        {
            _filterGenerator.LoadFiltersFromCsv();
            _filterGenerator.SendFiltersToSql();
            var filtersFromDB = LoadFiltersFromDB();

            string id = "Id";
            string name = "Name";
            string type = "Type";
            string currency = "Currency";
            string netprice = "Net";
            string grossprice = "Gross";

            Console.WriteLine($"{id}\t{name,-10}\t{type,20}\t{currency,10}\t{netprice}\t{grossprice}");

            foreach (var filter in filtersFromDB)
            {
                Console.WriteLine($"{filter.Id}\t{filter.Name,-10}\t{filter.Type,20}\t{filter.Currency,10}\t{filter.NetPrice}\t{filter.GrossPrice}");
            }
        }

        private List<Filter> LoadFiltersFromDB()
        {
            return _filterManagerAppDbContext.Filters.ToList();
        }

        public void AddNewFilter()
        {

            var newFilter = new Filter();

            while (true)
            {
                string answear = GetAnswerFromUser("Enter new filter name or q to exit:");
                if (answear == "q")
                {
                    break;
                }
                else
                {
                    newFilter.Name = answear;
                }


                string answear1 = GetAnswerFromUser("Enter type of modified filter or q to exit:");
                if (answear1 == "q")
                {
                    break;
                }
                else
                {
                    newFilter.Type = answear1;
                }


                string answear2 = GetAnswerFromUser("Enter currency of modified filter or q to exit:");
                if (answear2 == "q")
                {
                    break;
                }
                else
                {
                    newFilter.Currency = answear2;
                }

                newFilter.NetPrice = AskForNetPrice("Enter price of new filter:");

                decimal vat = (decimal)1.23;
                newFilter.GrossPrice = newFilter.NetPrice * vat;

                _filterManagerAppDbContext.Filters.Add(newFilter);
                _filterManagerAppDbContext.SaveChanges();
                ItemAdded?.Invoke(this, newFilter);
                break;
            }

        }    

        public void ModifyFilter()
        {
            while (true)
            {
                int id = AskForId();
                var modifyFilter = LoadFilterById(id);
                              
                string answear = GetAnswerFromUser("Enter modified filter name or q to exit:");
                if (answear == "q")
                {
                    break;
                }
                else
                {
                    modifyFilter.Name = answear;
                }
                                
                string answear1 = GetAnswerFromUser("Enter type of modified filter or q to exit:");
                if (answear1 == "q")
                {
                    break;
                }
                else
                {
                    modifyFilter.Type = answear1;
                }
                                
                string answear2 = GetAnswerFromUser("Enter currency of modified filter or q to exit:");
                if (answear2 == "q")
                {
                    break;
                }
                else
                {
                    modifyFilter.Currency = answear2;
                }

                modifyFilter.NetPrice = AskForNetPrice("Enter price for modified filter:");

                decimal vat = (decimal)1.23;
                modifyFilter.GrossPrice = modifyFilter.NetPrice * vat;
                
                _filterManagerAppDbContext.SaveChanges();
                ItemChanged?.Invoke(this, modifyFilter);
                break;
            }
        }

        public void DeleteFilter()
        {
            int id = AskForId();
            var deleteFilter = LoadFilterById(id);
            _filterManagerAppDbContext.Filters.Remove(deleteFilter);
            _filterManagerAppDbContext.SaveChanges();
            ItemRemoved?.Invoke(this, deleteFilter);
        }

        public void SaveFiltersToFile()
        {
            var filters = LoadFiltersFromDB();
            _fileWriters.SaveToJson(filters);
            _fileWriters.SaveToCsv(filters);
            _fileWriters.SaveToXml(filters);
            SavedToFile?.Invoke(this, EventArgs.Empty);
        }

        private Filter LoadFilterById(int id)
        {
            return _filterManagerAppDbContext.Filters.FirstOrDefault(x => x.Id == id);
        }

        private static string GetAnswerFromUser(string question)
        {
            Console.WriteLine($"\n{question}");
            string answear = Console.ReadLine();
            return answear;
        }

        private static decimal AskForNetPrice(string questionForPrice)
        {
            decimal netPrice;

            while (true)
            {
                Console.WriteLine($"{questionForPrice}");

                string answear = Console.ReadLine();

                if (decimal.TryParse(answear, out netPrice))
                {
                    return netPrice;
                }
                else
                {
                    Console.WriteLine("Entered price is not an decimal. Try one more time.");
                }
            }
        }

        private int AskForId()
        {
            bool inLoop = true;
            int id = default;
            int filtersMinId = _filterManagerAppDbContext.Filters.Min(x => x.Id);
            int filtersMaxId = _filterManagerAppDbContext.Filters.Max(x => x.Id);
            
            while (inLoop)
            {
                Console.WriteLine("\nEnter id of filter:");

                string answear = Console.ReadLine();

                if (int.TryParse(answear, out id))
                {
                    if (id < filtersMinId || id > filtersMaxId)
                    {
                        Console.WriteLine($"Entered id {id} is out of range. Enter id between {filtersMinId} and {filtersMaxId}.");
                    }
                    else if (LoadFilterById(id) == null) 
                    {
                        Console.WriteLine($"Entered id {id} doesn't exist. Choose one that exist.");
                    }
                    else
                    {
                        inLoop = false;
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Entered number is not an integer. Try one more time.");
                }
            }

            return id;
        }
    }
}
