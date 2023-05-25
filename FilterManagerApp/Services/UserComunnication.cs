using FilterManagerApp.Data.Entities;
using FilterManagerApp.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterManagerApp.Services
{
    public class UserComunnication : IUserCommunication
    {
        private readonly IFilterRepository _filter;
        private readonly IFilterRepository _filterRepository;

        public UserComunnication(IFilterRepository filter, IFilterRepository filterRepository)
        {
            _filter = filter;
            _filterRepository = filterRepository;
        }

        public void RunIntro()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("           ||| FilterManagerApp |||                    ");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"  This app alows you to view, modify, add or delete \n\t\tfilters in SQL Database");
            Console.ResetColor();
            
        }

        public void RunMenu()
        {
            bool inLoop = true;

            while (inLoop)
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine("\nEnter propper key to proceed: ");
                Console.ResetColor();
                Console.WriteLine($"1) See all filters.\n2) Add new filter. \n3) Modify filter.\n4) Delete filter. \n5) Save catalogue to json, csv, xml files. \nQ) Exit program.  ");

                var choose = Console.ReadKey().Key;

                if (choose == ConsoleKey.Q)
                {
                    Console.WriteLine("\nThanks for using CarService!");
                    inLoop = false;
                    break;
                }

                switch (choose)
                {
                    case ConsoleKey.D1 or ConsoleKey.NumPad1:
                        Console.WriteLine();
                        _filterRepository.WriteAllToConsole();
                        break;
                    case ConsoleKey.D2 or ConsoleKey.NumPad2:
                        _filterRepository.AddNewFilter();
                        break;
                    case ConsoleKey.D3 or ConsoleKey.NumPad3:
                        _filterRepository.ModifyFilter();                       
                        break;
                    case ConsoleKey.D4 or ConsoleKey.NumPad4:
                        _filterRepository.DeleteFilter();
                        break;
                    case ConsoleKey.D5 or ConsoleKey.NumPad5:
                        _filterRepository.SaveFiltersToFile();
                        break;
                    default:
                        Console.WriteLine("\nIInvalid key. Try to click correct key.");
                        break;
                }
            }
        }

    }
}
