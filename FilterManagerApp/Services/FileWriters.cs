using CsvHelper;
using FilterManagerApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FilterManagerApp.Services
{
    public class FileWriters<T> : IFileWriters<T> where T : class
    {
        public FilterManagerAppDbContext _filterManagerAppDbContext;

        public FileWriters(FilterManagerAppDbContext filterManagerAppDbContext)
        {
            _filterManagerAppDbContext = filterManagerAppDbContext;
        }
        public void SaveToJson(List<T> list)
        {
            string directoryPath = CreateDirectory("NewFiles");
            string jsonName = "filtr-catalogue.json";
            string fileJsonPath = directoryPath + "\\" + jsonName;
            string jsonString = JsonSerializer.Serialize(list);
            File.WriteAllText(fileJsonPath, jsonString);
        }

        public void SaveToCsv(List<T> list)
        {
            string directoryPath = CreateDirectory("NewFiles");
            string csvName = "filtr-catalogue.csv";
            string fileCsvPath = directoryPath + "\\" + csvName;
            using (var writer = new StreamWriter(fileCsvPath))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(list);
            }
        }

        public void SaveToXml(List<Filter> list)
        {
            var document = new XDocument();

            var objects = new XElement("Filters", list
                .Select(x =>
                new XElement("Filter",
                new XAttribute("Id", x.Id),
                new XAttribute("Name", x.Name),
                new XAttribute("Type", x.Type),
                new XAttribute("Currency", x.Currency),
                new XAttribute("NetPrice", x.NetPrice),
                new XAttribute("GrossPrice", x.GrossPrice))
                ));

            document.Add(objects);

            string directoryPath = CreateDirectory("NewFiles");
            string xmlName = "filtr-catalogue.xml";
            string fileXmlPath = directoryPath + "\\" + xmlName;

            document.Save(fileXmlPath);

        }

        public string CreateDirectory(string directoryName)
        {
            Directory.CreateDirectory(directoryName);
            string directoryPath = Path.GetFullPath(directoryName);
            return directoryPath;
        }


    }

}
