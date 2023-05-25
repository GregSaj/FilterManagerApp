using FilterManagerApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterManagerApp.Data.Repositories
{
    public class SqlRepository<T> : IRepository<T> where T : class, IEntity, new()
    {
        private readonly FilterManagerAppDbContext _dbContext;
        private readonly DbSet<T> _dbSet;
        
        public SqlRepository(FilterManagerAppDbContext dbContext) 
        {
            _dbContext = dbContext;
           
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.OrderBy(item => item.Id).ToList();
        }

        public T? GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public void Add(T item)
        {
            _dbSet.Add(item);          
        }

        public void Remove(T item)
        {
            _dbSet.Remove(item);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
        public int GetListCount()
        {
            return Read().ToList().Count;
        }               

        public IEnumerable<T> Read()
        {
            return _dbSet.ToList();
        }
    }
}
