using FilterManagerApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterManagerApp.Data.Repositories
{
    public interface IRepository<T> : IReadRepository<T>, IWriteRepository<T>
    where T : class, IEntity
    {       
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Add(T item);
        void Remove (T item);
        void Save();
    }
}
