using System.Collections.Generic;

namespace core.Repositories
{
    public interface IRepository<T> where T: class
    {
        IEnumerable<T> All();
        T FindById(long ID);
        void Add(T obj);
        void Update(T obj);
        void Delete(T obj);
    }
}
