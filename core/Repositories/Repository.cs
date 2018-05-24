using core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace core.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly AppDataContext Db;
        private DbSet<T> entities;

        public Repository(AppDataContext dbContext)
        {
            this.Db = dbContext;
            entities = dbContext.Set<T>();
        }

        public void Add(T obj)
        {
            if (obj == null)
                throw new ArgumentNullException("obj");
            entities.Add(obj);
        }

        public IEnumerable<T> All()
        {
            return entities.AsEnumerable();
        }

        public void Delete(T obj)
        {
            entities.Remove(obj);
        }

        public T FindById(long ID)
        {
            return entities.SingleOrDefault(d => d.Id == ID);
        }

        public void Update(T obj)
        {
            if (obj == null)
                throw new ArgumentNullException("obj");
        }
    }
}
