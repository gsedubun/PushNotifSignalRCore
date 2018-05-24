using core.Models;
using System;

namespace core.Repositories
{
    public class Unitofwork : IDisposable
    {
        private AppDataContext Db;

        public AkunUserRepository AkunUser { get; private set; }

        public Unitofwork(AppDataContext context)
        {
            this.Db = context;
            this.AkunUser = new AkunUserRepository(Db);

        }
        public int Save()
        {
           return Db.SaveChanges();

        }

        public void Dispose()
        {
            Save();
            Db.Dispose();
        }
    }
}
