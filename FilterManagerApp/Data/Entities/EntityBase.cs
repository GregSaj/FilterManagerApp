using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FilterManagerApp.Data.Entities
{
    public abstract class EntityBase: IEntity
    {
        public int Id { get; set; }        

    }
}
