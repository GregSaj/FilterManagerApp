using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterManagerApp.Data.Entities
{
    public class Filter: EntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Currency { get; set; }
        public decimal NetPrice { get; set; }
        public decimal GrossPrice { get; set; }
    }
}
