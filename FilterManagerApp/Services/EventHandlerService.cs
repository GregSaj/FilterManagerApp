using CsvHelper;
using FilterManagerApp.Data.Entities;
using FilterManagerApp.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace FilterManagerApp.Services
{
    public class EventHandlerService : IEventHandlerService
    {
        private readonly IFilterRepository _filterRepository;

        public EventHandlerService(IFilterRepository filterRepository)
        {
            _filterRepository = filterRepository;
        }

        public void SubscribeToEvents()
        {
            _filterRepository.ItemAdded += FilterRepositoryOnItemAdded;
            _filterRepository.ItemRemoved += FilterRepositoryOnItemRemoved;
            _filterRepository.ItemChanged += FilterRepositoryOnItemChanged;
            _filterRepository.SavedToFile += FilterRepositorySavedToFile;
        }

        private void FilterRepositorySavedToFile(object? sender, EventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"\nFilters saved successfully to files.\n");
            Console.ResetColor();

            using (var writer = File.AppendText(@"NewFiles\filter-actions.txt"))
            {
                writer.WriteLine($"{DateTime.UtcNow} Files saved.");
                writer.Dispose();
            }
        }

        private void FilterRepositoryOnItemAdded(object? sender, Filter e)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"Filter {e.Name} added successfully.\n");
            Console.ResetColor();

            using (var writer = File.AppendText(@"NewFiles\filter-actions.txt"))
            {
                writer.WriteLine($"{DateTime.UtcNow} Filter added, Id: {e.Id} Name: {e.Name}");
                writer.Dispose();
            }
        }

        private void FilterRepositoryOnItemRemoved(object? sender, Filter e)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"Filter {e.Name} removed successfully.\n");
            Console.ResetColor();

            using (var writer = File.AppendText(@"NewFiles\filter-actions.txt"))
            {
                writer.WriteLine($"{DateTime.UtcNow} Filter removed, Id: {e.Id} Name: {e.Name}");
                writer.Dispose();
            }

        }

        private void FilterRepositoryOnItemChanged(object? sender, Filter e)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"Filter {e.Name} updated successfully.\n");
            Console.ResetColor();

            using (var writer = File.AppendText(@"NewFiles\filter-actions.txt"))
            {
                writer.WriteLine($"{DateTime.UtcNow} Filter updated: {e.Id} {e.Name}");
                writer.Dispose();
            }
        }

    }

 

}

 